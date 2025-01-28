import { isPresent } from "utils/util";
import { Branch } from "types/api-basereservation";
import { useTypedApiClientBS } from "../useTypedApiClientBS";
import { useQuery, UseQueryResult } from "@tanstack/react-query";

export const useGetBranchById = (branchId: string | undefined): UseQueryResult<Branch> => {
    const getBranch = useTypedApiClientBS({
        path: '/api/Branch/{branchId}',
        method: 'get'
    })

    return useQuery({
        queryKey: ["GetBranch", branchId],
        queryFn: async () => {
            const { data } = await getBranch({ branchId: Number(branchId) });
            return data
        },
        enabled: isPresent(branchId),
        staleTime: 0,
    })
}
