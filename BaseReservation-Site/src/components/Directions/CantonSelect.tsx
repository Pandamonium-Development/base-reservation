import { useEffect } from "react"
import { Canton } from "types/api-basereservation"
import { ErrorProcess } from "components/Error/ErrorProcess"
import { useGetCantons } from "hooks/api-basereservation/useGetCantons"
import { CircularProgress, InputLabel, MenuItem, Select, Stack } from "@mui/material"

interface CantonSelectProps {
    selectedProvince: number
    selectedCanton: number
    onCantonChange: (cantonId: number) => void;
}

export const CantonSelect = ({ selectedProvince, selectedCanton, onCantonChange }: CantonSelectProps) => {
    const { data: cantons, isLoading, isError, refetch } = useGetCantons(selectedProvince)

    useEffect(() => {
        if (selectedProvince) {
            refetch();
        }
    }, [selectedProvince, refetch])

    if (isLoading) {
        return <CircularProgress />
    }

    if (isError) {
        return <ErrorProcess />
    }

    return (
        <Stack direction='column' gap={1}>
            <InputLabel htmlFor='canton' required>
                Cantón
            </InputLabel>
            <Select
                id='canton'
                value={selectedCanton != 0 && cantons?.length == 0 ? 0 : selectedCanton}
                onChange={(e) => onCantonChange(Number(e.target.value))}
            >
                <MenuItem key={0} value={0}>
                    Seleccione el cantón
                </MenuItem>
                {cantons && cantons.map((canton: Canton) => (
                    <MenuItem key={canton.id} value={canton.id}>
                        {canton.name}
                    </MenuItem>
                ))}
            </Select>
        </Stack>
    )
}