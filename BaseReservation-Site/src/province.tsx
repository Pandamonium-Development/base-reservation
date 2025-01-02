import { useGetProvinces } from './hooks/api-basereservation/useGetProvinces'

export const Province = () => {
    // eslint-disable-next-line react-hooks/rules-of-hooks
    const getProvinces = useGetProvinces()

    const provinces = getProvinces.data;
    console.log(provinces)

    return <p>Hola</p>
}