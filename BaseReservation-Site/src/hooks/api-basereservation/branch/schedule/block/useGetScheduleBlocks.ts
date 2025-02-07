import { isPresent } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";
import { BranchScheduleBlock } from "types/api-basereservation";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, useTypedApiClientBS } from "hooks/useTypedApiClientBS";

export const useGetScheduleBlocks = (scheduleId: string | undefined): UseQueryResult<Array<BranchScheduleBlock>, ApiError> => {
    const path = '/api/Schedule/{scheduleId}/Block';
    const method = 'get';

    const getScheduleBlocks = useTypedApiClientBS({ path, method })

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
