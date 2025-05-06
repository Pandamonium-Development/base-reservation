import { months } from "utils/util";
import { MonthName } from "types/api-basereservation"
import { FormControl, InputLabel, MenuItem, Select, Stack } from "@mui/material";

interface MonthSelectProps {
    selectedMonth: string,
    onMonthChange: (dia: MonthName) => void
    errorForm?: boolean;
}

export const MonthSelect = ({ selectedMonth, onMonthChange, errorForm }: MonthSelectProps) => {
    const isValidMonth = months.some(month => month === selectedMonth)
    const valueToShow = isValidMonth ? selectedMonth : ''

    return (
        <Stack direction='column' gap={1}>
            <InputLabel sx={{ fontSize: '1rem' }} htmlFor='mes' required>
                Mes
            </InputLabel>
            <FormControl fullWidth error={errorForm}>
                <Select
                    id='mes'
                    value={valueToShow}
                    onChange={(e) => onMonthChange(e.target.value as MonthName)}
                >
                    {months.map((month) => (
                        <MenuItem key={month} value={month}>
                            {month}
                        </MenuItem>
                    ))}

                </Select>
            </FormControl>
        </Stack>
    )
}