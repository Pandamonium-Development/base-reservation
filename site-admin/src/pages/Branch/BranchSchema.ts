import { telephoneMaskRegex } from "utils/util";
import { number, object, string, type InferType } from "yup";

export const BranchDefaultValues = {
    id: 0,
    name: '',
    description: '',
    telephone: '',
    email: '',
    provinceId: 0,
    cantonId: 0,
    districtId: 0,
    address: '',
};

export const BranchSchema = object().shape({
    id: number(),
    name: string().required('Ingrese el nombre de la sucursal').max(80),
    description: string().required('Ingresa la descripción de la sucursal').max(150),
    telephone: string().matches(telephoneMaskRegex, { message: 'Ingrese un número de teléfono válido' }).required('Ingrese el número de teléfono').max(9),
    email: string().email('Ingrese un correo electrónico válido').required('Ingrese el correo electrónico').max(50),
    provinceId: number(),
    cantonId: number(),
    districtId: number().required('Seleccione el distrito').min(1, 'Seleccione el distrito'),
    address: string().max(250),
})

export type BranchForm = InferType<typeof BranchSchema>

