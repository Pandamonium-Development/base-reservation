import { Canton } from "types/api-basereservation";
import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, useTypedApiClientBS } from "../useTypedApiClientBS";

export const useGetCantons = (provinceId: number): UseQueryResult<Array<Canton>, ApiError> => {
    const path = '/api/Province/{provinceId}/Canton';
    const method = 'get';

    const getCantons = useTypedApiClientBS({ path, method })

    return useQuery({
        queryKey: ["Cantons"],
        queryFn: async () => {
            const { data } = await getCantons(castRequestBody({ provinceId: provinceId }, path, method));
            return data
        },
        enabled: true
    })
}
