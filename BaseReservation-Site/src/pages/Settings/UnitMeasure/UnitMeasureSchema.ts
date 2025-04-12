import { InferType, number, object, string } from "yup";

export const UnitMeasureDefaultValues = {
    id: 0,
    name: '',
    symbol: '',
};

export const UnitMeasureSchema = object().shape({
    id: number(),
    name: string().required('El nombre es requerido').max(25, 'El nombre no puede tener más de 25 caracteres'),
    symbol: string()
        .required('El símbolo es requerido')
        .max(5, 'El símbolo no puede tener más de 5 caracteres')
})

export type UnitMeasureForm = InferType<typeof UnitMeasureSchema>

