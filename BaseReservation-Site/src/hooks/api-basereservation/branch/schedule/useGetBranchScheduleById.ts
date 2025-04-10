import { isPresent } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";
import { BranchSchedule } from "types/api-basereservation";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBS } from "hooks/UseTypedApiClientBS";

export const UseGetBranchScheduleById = (branchScheduleId: string | undefined): UseQueryResult<BranchSchedule, ApiError> => {
    const path = '/api/BranchSchedule/{branchScheduleId}';
    const method = 'get';

    const getBranchSchedule = UseTypedApiClientBS({ path, method })

    return useQuery({
        queryKey: ["GetBranchSchedule", branchScheduleId],
        queryFn: async () => {
            const { data } = await getBranchSchedule(castRequestBody({ branchScheduleId: Number(branchScheduleId) }, path, method));
            return data
        },
        retry: false,
        enabled: isPresent(branchScheduleId),
        staleTime: 0,
    })
}
