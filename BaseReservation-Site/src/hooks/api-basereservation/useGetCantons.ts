import { Canton } from "types/api-basereservation";
import { useTypedApiClientBS } from "../useTypedApiClientBS";
import { useQuery, UseQueryResult } from "@tanstack/react-query";

export const useGetCantons = (provinceId: number): UseQueryResult<Array<Canton>> => {
    const getCantons = useTypedApiClientBS({
        path: '/api/Province/{provinceId}/Canton',
        method: 'get'
    })

    return useQuery({
        queryKey: ["Cantons"],
        queryFn: async () => {
            const { data } = await getCantons({ provinceId: provinceId });
            return data
        },
        enabled: true
    })
}
