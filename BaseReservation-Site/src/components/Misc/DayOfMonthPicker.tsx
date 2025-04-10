import { TextField } from "@mui/material";
import { HolidayForm } from "pages/Settings/Holiday/HolidaySchema";
import { ControllerRenderProps } from "react-hook-form";
import { MonthName } from "types/api-basereservation";

const daysInMonth: Record<MonthName, number> = {
    Enero: 31,
    Febrero: 29,
    Marzo: 31,
    Abril: 30,
    Mayo: 31,
    Junio: 30,
    Julio: 31,
    Agosto: 31,
    Septiembre: 30,
    Octubre: 31,
    Noviembre: 30,
    Diciembre: 31,
};

interface DayOfMonthPickerProps {
    field: ControllerRenderProps<HolidayForm, 'day'>;
    selectedMonth: MonthName;
    error?: string;
}

export const DayOfMonthPicker = ({ field, selectedMonth, error }: DayOfMonthPickerProps) => {
    const maxDay = daysInMonth[selectedMonth];

    return (
        <TextField
            required
            type="number"
            label="Día del mes"
            fullWidth
            inputProps={{
                min: 1,
                max: maxDay,
            }}
            error={!!error}
            {...field}
        />
    );
};