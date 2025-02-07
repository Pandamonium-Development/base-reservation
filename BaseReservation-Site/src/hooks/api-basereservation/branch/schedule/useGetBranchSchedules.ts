import { isPresent } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";
import { BranchSchedule } from "types/api-basereservation";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, useTypedApiClientBS } from "hooks/useTypedApiClientBS";

export const useGetBranchSchedules = (branchId: string | undefined): UseQueryResult<Array<BranchSchedule>, ApiError> => {
    const path = '/api/Branch/{branchId}/Schedule';
    const method = 'get';

    const getBranchSchedules = useTypedApiClientBS({ path, method })

    return useQuery({
        queryKey: ["GetBranchSchedules"],
        queryFn: async () => {
            const { data } = await getBranchSchedules(castRequestBody({ branchId: Number(branchId) }, path, method));
            return data
        },
        retry: false,
        enabled: isPresent(branchId),
        staleTime: 0,
    })
}
