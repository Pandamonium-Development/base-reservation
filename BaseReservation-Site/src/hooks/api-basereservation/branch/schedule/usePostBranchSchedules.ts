import { transformErrorKeys } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { castRequestBody, useTypedApiClientBS } from "hooks/useTypedApiClientBS";
import { BaseReservationErrorDetails, BranchScheduleRequest } from "types/api-basereservation";

interface usePostBranchSchedulesProps {
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

export const usePostBranchSchedules = ({
    branchId,
    onSuccess,
    onError,
    onSettled
}: usePostBranchSchedulesProps) => {
    const path = `/api/Branch/{branchId}/Schedule`;
    const method = 'post';

    const postBranchSchedules = useTypedApiClientBS({ path, method })
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