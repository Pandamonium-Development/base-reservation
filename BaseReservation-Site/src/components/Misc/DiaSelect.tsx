import { weekDays, weekDaysSpanish } from "utils/util";
import { WeeklyDay } from "types/api-basereservation"
import { FormControl, InputLabel, MenuItem, Select, Stack } from "@mui/material";

interface DiaSelectProps {
    selectedDay: string,
    onDayChange: (dia: WeeklyDay) => void
    errorForm?: boolean;
}

export const DiaSelect = ({ selectedDay, onDayChange, errorForm }: DiaSelectProps) => {
    const isValidProvince = weekDays.some(dia => dia === selectedDay)
    const valueToShow = isValidProvince ? selectedDay : ''

    return (
        <Stack direction='column' gap={1}>
            <InputLabel sx={{ fontSize: '1rem' }} htmlFor='dia' required>
                Día
            </InputLabel>
            <FormControl fullWidth error={errorForm}>
                <Select
                    id='dia'
                    value={valueToShow}
                    onChange={(e) => onDayChange(e.target.value as WeeklyDay)}
                >
                    {weekDays.map((day, index) => (
                        <MenuItem key={day} value={day}>
                            {weekDaysSpanish[index]}
                        </MenuItem>
                    ))}

                </Select>
            </FormControl>
        </Stack>
    )
}