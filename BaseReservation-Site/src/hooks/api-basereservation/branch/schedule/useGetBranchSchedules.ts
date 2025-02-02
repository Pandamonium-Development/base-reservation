import { isPresent } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";
import { BranchSchedule } from "types/api-basereservation";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, useTypedApiClientBS } from "hooks/useTypedApiClientBS";

export const useGetBranchSchedules = (branchId: number): UseQueryResult<Array<BranchSchedule>, ApiError> => {
    const path = '/api/Branch/{branchId}/Schedule';
    const method = 'get';

    const getBranchSchedules = useTypedApiClientBS({ path, method })

    return useQuery({
        queryKey: ["GetSchedules"],
        queryFn: async () => {
            const { data } = await getBranchSchedules(castRequestBody({ branchId }, path, method));
            return data
        },
        enabled: isPresent(branchId)
    })
}
