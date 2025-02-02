import { isPresent } from "utils/util";
import { Branch } from "types/api-basereservation";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, useTypedApiClientBS } from "hooks/useTypedApiClientBS";

export const useGetBranchById = (branchId: string | undefined): UseQueryResult<Branch> => {
    const getBranch = useTypedApiClientBS({
        path: '/api/Branch/{branchId}',
        method: 'get'
    })

    return useQuery({
        queryKey: ["GetBranch", branchId],
        queryFn: async () => {
            const { data } = await getBranch(castRequestBody({ branchId: Number(branchId) }, "/api/Branch/{branchId}", "get"));
            return data
        },
        retry: false,
        enabled: isPresent(branchId),
        staleTime: 0,
    })
}
