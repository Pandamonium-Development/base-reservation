import { useEffect } from "react"
import { District } from "types/api-basereservation"
import { ErrorProcess } from "components/Error/ErrorProcess"
import { useGetDistricts } from "hooks/api-basereservation/useGetDistricts"
import { CircularProgress, FormControl, InputLabel, MenuItem, Select, Stack } from "@mui/material"

interface DistrictSelectProps {
    selectedProvince: number;
    selectedCanton: number;
    selectedDistrict: number;
    onDistrictChange: (cantonId: number) => void;
    error?: boolean;
}

export const DistrictSelect = ({ selectedProvince, selectedCanton, selectedDistrict, onDistrictChange, error }: DistrictSelectProps) => {
    const { data: districts, isLoading, isError, refetch } = useGetDistricts(selectedCanton)

    useEffect(() => {
        if (selectedProvince || selectedCanton) {
            refetch();
        }
    }, [selectedProvince, selectedCanton, refetch])

    if (isLoading) {
        return <CircularProgress />
    }

    if (isError) {
        return <ErrorProcess />
    }

    return (
        <Stack direction='column' gap={1}>
            <InputLabel htmlFor='district' required>
                Distrito
            </InputLabel>
            <FormControl fullWidth error={error}>
                <Select
                    id='district'
                    value={selectedDistrict != 0 && districts?.length == 0 ? 0 : selectedDistrict}
                    onChange={(e) => onDistrictChange(Number(e.target.value))}
                >
                    <MenuItem key={0} value={0}>
                        Seleccione el distrito
                    </MenuItem>
                    {districts && districts.map((district: District) => (
                        <MenuItem key={district.id} value={district.id}>
                            {district.name}
                        </MenuItem>
                    ))}
                </Select>
            </FormControl>
        </Stack>
    )
}