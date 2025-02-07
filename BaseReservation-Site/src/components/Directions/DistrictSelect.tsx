import { useEffect, useState } from "react"
import { District } from "types/api-basereservation"
import { ErrorProcess } from "components/Error/ErrorProcess"
import { useGetDistricts } from "hooks/api-basereservation/useGetDistricts"
import { FormControl, InputLabel, MenuItem, Select, Stack } from "@mui/material"
import { CircularLoadingProgress } from "components/LoadingProgress/CircularLoadingProcess"
import { useSnackbar } from "stores/useSnackbar"

interface DistrictSelectProps {
    selectedProvince: number;
    selectedCanton: number;
    selectedDistrict: number;
    onDistrictChange: (districtId: number) => void;
    errorForm?: boolean;
}

export const DistrictSelect = ({ selectedProvince, selectedCanton, selectedDistrict, onDistrictChange, errorForm }: DistrictSelectProps) => {
    const { data: districts, isLoading, isError, refetch, error } = useGetDistricts(selectedCanton)
    const [localSelectedDistrict, setLocalSelectedDistrict] = useState<number>(selectedDistrict);
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    useEffect(() => {
        if (selectedProvince || selectedCanton) {
            refetch();
        }
    }, [selectedProvince, selectedCanton, refetch])

    const isValidDistrict = districts?.some(district => district.id === selectedDistrict);

    useEffect(() => {
        if (districts && districts.length > 0 && !isValidDistrict) {
            setLocalSelectedDistrict(0);
        } else {
            setLocalSelectedDistrict(selectedDistrict);
        }
    }, [districts, selectedDistrict, isValidDistrict]);

    useEffect(() => {
        if (isError) {
            setSnackbarMessage(error.message, 'error');
        }
    }, [isError, setSnackbarMessage, error])

    if (isLoading) {
        return <CircularLoadingProgress />
    }

    if (isError) {
        return <ErrorProcess />
    }

    return (
        <Stack direction='column' gap={1}>
            <InputLabel sx={{ fontSize: '1rem' }} htmlFor='district' required>
                Distrito
            </InputLabel>
            <FormControl fullWidth error={errorForm}>
                <Select
                    id='district'
                    value={localSelectedDistrict}
                    onChange={(e) => {
                        const newDistrict = Number(e.target.value);
                        setLocalSelectedDistrict(newDistrict);
                        onDistrictChange(newDistrict);
                    }}
                >
                    <MenuItem key={0} value={0}>
                        Seleccione el distrito
                    </MenuItem>
                    {districts?.map((district: District) => (
                        <MenuItem key={district.id} value={district.id}>
                            {district.name}
                        </MenuItem>
                    ))}
                </Select>
            </FormControl>
        </Stack>
    )
}
