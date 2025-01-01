import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { useTypedApiClientBS } from "../useTypedApiClientBS";
import { Province } from "../../types/api-basereservation";

export const useGetProvinces = (): UseQueryResult<Array<Province>> => {
    const getProvinces = useTypedApiClientBS({
        path: '/api/Province',
        method: 'get'
    })

    return useQuery({
        queryKey: ["Provinces"],
        queryFn: async () => {
            const { data } = await getProvinces({});
            return data
        },
        enabled: true
    })
}
