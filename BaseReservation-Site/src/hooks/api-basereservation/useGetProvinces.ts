import { Province } from "types/api-basereservation";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, useTypedApiClientBS } from "../useTypedApiClientBS";

export const useGetProvinces = (): UseQueryResult<Array<Province>> => {
    const getProvinces = useTypedApiClientBS({
        path: '/api/Province',
        method: 'get'
    })

    return useQuery({
        queryKey: ["Provinces"],
        queryFn: async () => {
            const { data } = await getProvinces(castRequestBody({}, "/api/Province", "get"));
            return data
        },
        enabled: true
    })
}
