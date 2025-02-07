import { timeRegex } from "utils/util";
import { InferType, number, object, string } from "yup";

export const BlockDefaultValues = {
    id: 0,
    branchScheduleId: 0,
    startHour: '',
    endHour: ''
};

export const BlockSchema = object().shape({
    id: number(),
    branchScheduleId: number(),
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

export type BlockForm = InferType<typeof BlockSchema>