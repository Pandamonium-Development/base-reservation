import { isPresent } from "utils/util";
import { Branch } from "types/api-basereservation";
import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBS } from "hooks/UseTypedApiClientBS";

export const UseGetBranchById = (branchId: string | undefined): UseQueryResult<Branch, ApiError> => {
    const path = '/api/Branch/{branchId}';
    const method = 'get';

    const getBranch = UseTypedApiClientBS({ path, method })

    return useQuery({
        queryKey: ["GetBranch", branchId],
        queryFn: async () => {
            const { data } = await getBranch(castRequestBody({ branchId: Number(branchId) }, path, method));
            return data
        },
        retry: false,
        enabled: isPresent(branchId),
        staleTime: 0,
    })
}
