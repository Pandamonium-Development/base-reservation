import { InferType, number, object, string } from "yup";

export const TaxDefaultValues = {
    id: 0,
    name: '',
    rate: 0,
};

export const TaxSchema = object().shape({
    id: number(),
    name: string().required('El nombre es requerido').max(40, 'El nombre no puede tener más de 40 caracteres'),
    rate: number()
        .transform((value, originalValue) =>
            originalValue === '' ? undefined : value
        )
        .required('La tasa es requerida')
        .min(0, 'La tasa no puede ser menor a 0')
        .max(100, 'La tasa no puede ser mayor a 100'),
})

export type TaxForm = InferType<typeof TaxSchema>

