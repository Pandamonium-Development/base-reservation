import { District } from "types/api-basereservation";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, useTypedApiClientBS } from "../useTypedApiClientBS";

export const useGetDistricts = (cantonId: number): UseQueryResult<Array<District>> => {
    const getDistricts = useTypedApiClientBS({
        path: '/api/Canton/{cantonId}/District',
        method: 'get'
    })

    return useQuery({
        queryKey: ["Districts"],
        queryFn: async () => {
            const { data } = await getDistricts(castRequestBody({ cantonId: cantonId }, '/api/Canton/{cantonId}/District', 'get'));
            return data
        },
        enabled: true
    })
}
