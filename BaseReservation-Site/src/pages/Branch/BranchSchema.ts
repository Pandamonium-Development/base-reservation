import { telephoneMaskRegex } from "utils/util";
import { bool, number, object, string, type InferType } from "yup";

export const BranchDefaultValues = {
    name: '',
    description: '',
    telephone: '',
    email: '',
    province: 0,
    canton: 0,
    districtId: 0,
    address: '',
    active: true,
};

export const BranchSchema = object().shape({
    name: string().required('Ingrese el nombre de la sucursal').max(80),
    description: string().required('Ingresa la descripción de la sucursal').max(150),
    telephone: string().matches(telephoneMaskRegex, { message: 'Ingrese un número de teléfono válido' }).required('Ingrese el número de teléfono').max(9),
    email: string().email('Ingrese un correo electrónico válido').required('Ingrese el correo electrónico').max(50),
    districtId: number().required('Seleccione el distrito').min(1, 'Seleccione el distrito'),
    address: string().max(250),
    active: bool().required('Seleccione el estado de la sucursal')
})

export type BranchForm = InferType<typeof BranchSchema>

