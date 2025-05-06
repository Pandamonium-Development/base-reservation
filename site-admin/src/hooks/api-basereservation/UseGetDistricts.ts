import { ApiError } from "openapi-typescript-fetch";
import { District } from "types/api-basereservation";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBS } from "../UseTypedApiClientBS";

export const UseGetDistricts = (cantonId: number): UseQueryResult<Array<District>, ApiError> => {
    const path = '/api/Canton/{cantonId}/District';
    const method = 'get';

    const getDistricts = UseTypedApiClientBS({ path, method })

    return useQuery({
        queryKey: ["Districts"],
        queryFn: async () => {
            const { data } = await getDistricts(castRequestBody({ cantonId: cantonId }, path, method));
            return data
        },
        enabled: true
    })
}
