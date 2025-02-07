import { isPresent } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";
import { BranchScheduleBlock } from "types/api-basereservation";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, useTypedApiClientBS } from "hooks/useTypedApiClientBS";

export const useGetScheduleBlockById = (branchScheduleBlockId: string | undefined): UseQueryResult<BranchScheduleBlock, ApiError> => {
    const path = '/api/BranchScheduleBlock/{branchScheduleßlockId}';
    const method = 'get';

    const getBlock = useTypedApiClientBS({ path, method })

    return useQuery({
        queryKey: ["GetBranchScheduleBlock", branchScheduleBlockId],
        queryFn: async () => {
            const { data } = await getBlock(castRequestBody({ branchScheduleßlockId: Number(branchScheduleBlockId) }, path, method));
            return data
        },
        retry: false,
        enabled: isPresent(branchScheduleBlockId),
        staleTime: 0,
    })
}
