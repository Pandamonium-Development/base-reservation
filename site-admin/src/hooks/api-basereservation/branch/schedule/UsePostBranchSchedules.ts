import { transformErrorKeys } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBS } from "hooks/UseTypedApiClientBS";
import { BaseReservationErrorDetails, BranchScheduleRequest } from "types/api-basereservation";

interface UsePostBranchSchedulesProps {
    branchId: number,
    onSuccess?: (
        data: boolean,
        variables: BranchScheduleRequest[]
    ) => void,
    onError?: (
        data: BaseReservationErrorDetails,
        variables: BranchScheduleRequest[]
    ) => void,
    onSettled?: (
        data: boolean | undefined,
        error: BaseReservationErrorDetails | null,
        variables: BranchScheduleRequest[]
    ) => void
}

export const UsePostBranchSchedules = ({
    branchId,
    onSuccess,
    onError,
    onSettled
}: UsePostBranchSchedulesProps) => {
    const path = `/api/Branch/{branchId}/Schedule`;
    const method = 'post';

    const postBranchSchedules = UseTypedApiClientBS({ path, method })
    const queryClient = useQueryClient();

    const createBranchSchedulesMutation = useMutation({
        mutationKey: ['PostBranchSchedules'],
        mutationFn: async (branchSchedules: Array<BranchScheduleRequest>) => {
            const { data } = await postBranchSchedules(castRequestBody({ branchId, branchSchedule: branchSchedules }, path, method))
            return data;
        },
        onSuccess: async (data: boolean, variables: BranchScheduleRequest[]) => {
            await queryClient.invalidateQueries({
                queryKey: ['GetBranchSchedule']
            })
            onSuccess?.(data, variables)
        },
        onError: (error: ApiError, _) => {
            onError?.(transformErrorKeys(error.data) as BaseReservationErrorDetails, _)
        },
        onSettled: (data, error, variables) => {
            onSettled?.(data, error, variables)
        }
    })

    return createBranchSchedulesMutation;
}