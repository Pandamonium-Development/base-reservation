import { Province } from "types/api-basereservation";
import { useTypedApiClientBS } from "../useTypedApiClientBS";
import { useQuery, UseQueryResult } from "@tanstack/react-query";

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
