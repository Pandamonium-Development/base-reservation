import { useEffect, useState } from "react"
import { Province } from "types/api-basereservation"
import { ErrorProcess } from "components/Error/ErrorProcess"
import { InputLabel, MenuItem, Select, Stack } from "@mui/material"
import { useGetProvinces } from "hooks/api-basereservation/useGetProvinces"
import { CircularLoadingProgress } from "components/LoadingProgress/CircularLoadingProcess"

interface ProvinceSelectProps {
    selectedProvince: number;
    onProvinceChange: (provinceId: number) => void;
}

export const ProvinceSelect = ({ selectedProvince, onProvinceChange }: ProvinceSelectProps) => {
    const [provinces, setProvinces] = useState<Array<Province>>([])
    const provinceItemsQuery = useGetProvinces()

    useEffect(() => {
        if (provinceItemsQuery.data) {
            setProvinces(provinceItemsQuery.data)
        }
    }, [provinceItemsQuery.data, selectedProvince])

    const isValidProvince = provinces.some(province => province.id === selectedProvince)
    const valueToShow = isValidProvince ? selectedProvince : 0

    if (provinceItemsQuery.isPending) {
        return <CircularLoadingProgress />
    }

    if (provinceItemsQuery.isError) {
        return <ErrorProcess />
    }

    return (
        <Stack direction='column' gap={1}>
            <InputLabel sx={{ fontSize: '1rem' }} htmlFor='province' required>
                Provincia
            </InputLabel>
            <Select
                id='province'
                value={valueToShow}
                onChange={(e) => onProvinceChange(Number(e.target.value))}
            >
                <MenuItem key={0} value={0}>
                    Seleccione la provincia
                </MenuItem>
                {provinces?.map((province) => (
                    <MenuItem key={province.id} value={province.id}>
                        {province.name}
                    </MenuItem>
                ))}
            </Select>
        </Stack>
    )
}