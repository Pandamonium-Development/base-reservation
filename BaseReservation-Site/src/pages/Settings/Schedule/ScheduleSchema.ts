import { weekDays } from "utils/util";
import { WeeklyDay } from "types/api-basereservation";
import { mixed, number, object, type InferType } from "yup";
import { endHourValidation, hourValidation } from "components/Shared/SchemaValidations";

export const ScheduleDefaultValues = {
    id: 0,
    day: 'Lunes',
    startHour: '',
    endHour: ''
};

export const ScheduleSchema = object().shape({
    id: number(),
    day: mixed<WeeklyDay>().oneOf(weekDays, 'Día no válido').required('El día es requerido'),
    startHour: hourValidation,
    endHour: endHourValidation
})

export type ScheduleForm = InferType<typeof ScheduleSchema>

