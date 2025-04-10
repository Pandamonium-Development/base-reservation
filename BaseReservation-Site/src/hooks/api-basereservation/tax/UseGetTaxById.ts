import { isPresent } from "utils/util";
import { Tax } from "types/api-basereservation";
import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBS } from "hooks/UseTypedApiClientBS";

export const UseGetTaxById = (taxId: string | undefined): UseQueryResult<Tax, ApiError> => {
    const path = '/api/Tax/{taxId}';
    const method = 'get';

    const getTax = UseTypedApiClientBS({ path, method })

    return useQuery({
        queryKey: ["GetTax", taxId],
        queryFn: async () => {
            const { data } = await getTax(castRequestBody({ taxId: Number(taxId) }, path, method));
            return data
        },
        retry: false,
        enabled: isPresent(taxId),
        staleTime: 0,
    })
}
