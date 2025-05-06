import { months } from "utils/util";
import { MonthName } from "types/api-basereservation";
import { mixed, number, object, string, type InferType } from "yup";

export const HolidayDefaultValues = {
    id: 0,
    name: '',
    month: 'Enero',
    day: 0,
};

export const HolidaySchema = object().shape({
    id: number(),
    name: string().required('El nombre es requerido').max(80, 'El nombre no puede exceder los 50 caracteres'),
    month: mixed<MonthName>().oneOf(months, 'Mes no válido').required('El mes es requerido'),
    day: number().required('El día es requerido').min(1, 'El día no puede ser menor a 1').max(31, 'El día no puede ser mayor a 31'),
})

export type HolidayForm = InferType<typeof HolidaySchema>

