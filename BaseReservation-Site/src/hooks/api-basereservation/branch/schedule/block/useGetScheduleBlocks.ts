import { isPresent } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";
import { BranchScheduleBlock } from "types/api-basereservation";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBS } from "hooks/UseTypedApiClientBS";

export const UseGetScheduleBlocks = (scheduleId: string | undefined): UseQueryResult<Array<BranchScheduleBlock>, ApiError> => {
    const path = '/api/Schedule/{scheduleId}/Block';
    const method = 'get';

    const getScheduleBlocks = UseTypedApiClientBS({ path, method })

    return useQuery({
        queryKey: ["GetScheduleBlocks"],
        queryFn: async () => {
            const { data } = await getScheduleBlocks(castRequestBody({ branchId: Number(scheduleId) }, path, method));
            return data
        },
        retry: false,
        enabled: isPresent(scheduleId),
        staleTime: 0,
    })
}
