import { Canton } from "types/api-basereservation";
import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBS } from "../UseTypedApiClientBS";

export const UseGetCantons = (provinceId: number): UseQueryResult<Array<Canton>, ApiError> => {
    const path = '/api/Province/{provinceId}/Canton';
    const method = 'get';

    const getCantons = UseTypedApiClientBS({ path, method })

    return useQuery({
        queryKey: ["Cantons"],
        queryFn: async () => {
            const { data } = await getCantons(castRequestBody({ provinceId: provinceId }, path, method));
            return data
        },
        enabled: true
    })
}
