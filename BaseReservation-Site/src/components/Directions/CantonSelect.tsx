import { useEffect, useRef, useState } from "react"
import { Canton } from "types/api-basereservation"
import { ErrorProcess } from "components/Error/ErrorProcess"
import { InputLabel, MenuItem, Select, Stack } from "@mui/material"
import { useGetCantons } from "hooks/api-basereservation/useGetCantons"
import { CircularLoadingProgress } from "components/LoadingProgress/CircularLoadingProcess"

interface CantonSelectProps {
    selectedProvince: number
    selectedCanton: number
    onCantonChange: (cantonId: number) => void;
}

export const CantonSelect = ({ selectedProvince, selectedCanton, onCantonChange }: CantonSelectProps) => {
    const { data: cantons, isLoading, isError, refetch } = useGetCantons(selectedProvince)
    const [localSelectedCanton, setLocalSelectedCanton] = useState<number>(selectedCanton);

    const initialCantonRef = useRef<number>(selectedCanton);

    useEffect(() => {
        if (selectedProvince) {
            refetch();
        }
    }, [selectedProvince, refetch]);

    useEffect(() => {
        if (cantons && cantons.length > 0) {
            if (!cantons.some(canton => canton.id === selectedCanton)) {
                setLocalSelectedCanton(0);
            } else {
                if (initialCantonRef.current === selectedCanton) {
                    setLocalSelectedCanton(selectedCanton);
                }
            }
        } else {
            setLocalSelectedCanton(0);
        }
    }, [cantons, selectedCanton]);

    if (isLoading) {
        return <CircularLoadingProgress />;
    }

    if (isError) {
        return <ErrorProcess />;
    }

    return (
        <Stack direction='column' gap={1}>
            <InputLabel sx={{ fontSize: '1rem' }} htmlFor='canton' required>
                Cantón
            </InputLabel>
            <Select
                id='canton'
                value={localSelectedCanton}
                onChange={(e) => {
                    const newCanton = Number(e.target.value);
                    setLocalSelectedCanton(newCanton);
                    onCantonChange(newCanton);
                }}
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