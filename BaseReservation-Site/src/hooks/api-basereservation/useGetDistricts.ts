import { District } from "types/api-basereservation";
import { useTypedApiClientBS } from "../useTypedApiClientBS";
import { useQuery, UseQueryResult } from "@tanstack/react-query";

export const useGetDistricts = (cantonId: number): UseQueryResult<Array<District>> => {
    const getDistricts = useTypedApiClientBS({
        path: '/api/Canton/{cantonId}/District',
        method: 'get'
    })

    return useQuery({
        queryKey: ["Districts"],
        queryFn: async () => {
            const { data } = await getDistricts({ cantonId: cantonId });
            return data
        },
        enabled: true
    })
}
