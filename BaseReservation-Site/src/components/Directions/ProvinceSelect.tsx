import { CircularProgress, InputLabel, MenuItem, Select, Stack } from "@mui/material"
import { ErrorProcess } from "components/Error/ErrorProcess"
import { useGetProvinces } from "hooks/api-basereservation/useGetProvinces"
import { useEffect, useState } from "react"
import { Province } from "types/api-basereservation"

interface ProvinceSelectProps {
    selectedProvince: number
    onProvinceChange: (provinceId: number) => void;
}

export const ProvinceSelect = ({ selectedProvince, onProvinceChange }: ProvinceSelectProps) => {
    const [provinces, setProvinces] = useState<Array<Province>>([])
    const provinceItemsQuery = useGetProvinces()

    useEffect(() => {
        if (provinceItemsQuery.data) {
            setProvinces(provinceItemsQuery.data)
        }
    }, [provinceItemsQuery.data, onProvinceChange, selectedProvince])

    if (provinceItemsQuery.isPending) {
        return <CircularProgress />
    }

    if (provinceItemsQuery.isError) {
        return <ErrorProcess />
    }

    return (
        <Stack direction='column' gap={1}>
            <InputLabel htmlFor='province' required>
                Provincia
            </InputLabel>
            <Select
                id='province'
                value={selectedProvince != 0 && provinces.length == 0 ? 0 : selectedProvince}
                onChange={(e) => onProvinceChange(Number(e.target.value))}
            >
                <MenuItem key={0} value={0}>
                    Seleccione la provincia
                </MenuItem>
                {provinces.map((province) => (
                    <MenuItem key={province.id} value={province.id}>
                        {province.name}
                    </MenuItem>
                ))}
            </Select>
        </Stack>
    )
}