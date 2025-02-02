import { Canton } from "types/api-basereservation";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, useTypedApiClientBS } from "../useTypedApiClientBS";

export const useGetCantons = (provinceId: number): UseQueryResult<Array<Canton>> => {
    const getCantons = useTypedApiClientBS({
        path: '/api/Province/{provinceId}/Canton',
        method: 'get'
    })

    return useQuery({
        queryKey: ["Cantons"],
        queryFn: async () => {
            const { data } = await getCantons(castRequestBody({ provinceId: provinceId }, '/api/Province/{provinceId}/Canton', 'get'));
            return data
        },
        enabled: true
    })
}
