import { timeRegex, weekDays } from "utils/util";
import { WeeklyDay } from "types/api-basereservation";
import { mixed, number, object, string, type InferType } from "yup";

export const ScheduleDefaultValues = {
    id: 0,
    day: '',
    startHour: '',
    endHour: ''
};

export const ScheduleSchema = object().shape({
    id: number(),
    day: mixed<WeeklyDay>().oneOf(weekDays as WeeklyDay[], 'Día no válido').required('El día es requerido'),
    startHour: string()
        .matches(timeRegex, 'Hora de inicio debe contener el formato de 24 horas (HH:mm)')
        .required('Hora de incio es requerida'),
    endHour: string()
        .matches(timeRegex, 'Hora de fin debe contene el formato de 24 horas (HH:mm)')
        .required('Hora de fin es requerida')
        .test('is-after-start', 'Hora de fin no puede estar antes de la hora de inicio', function (value) {
            const { startHour } = this.parent;

            const [startHours, startMinutes] = startHour.split(':').map(Number);
            const [endHours, endMinutes] = value.split(':').map(Number);

            const startTotalMinutes = startHours * 60 + startMinutes;
            const endTotalMinutes = Number(endHours) * 60 + Number(endMinutes);

            return endTotalMinutes > startTotalMinutes;
        })
})

export type ScheduleForm = InferType<typeof ScheduleSchema>

