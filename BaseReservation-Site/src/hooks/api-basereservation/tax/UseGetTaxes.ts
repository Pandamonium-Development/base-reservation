import { Tax } from "types/api-basereservation";
import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBS } from "hooks/UseTypedApiClientBS";

export const UseGetTaxes = (): UseQueryResult<Array<Tax>, ApiError> => {
    const path = '/api/Tax';
    const method = 'get';

    const getTaxes = UseTypedApiClientBS({ path, method })

    return useQuery({
        queryKey: ["GetTaxes"],
        queryFn: async () => {
            const { data } = await getTaxes(castRequestBody({}, path, method));
            return data
        },
        enabled: true
    })
}
