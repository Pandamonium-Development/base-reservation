import { transformErrorKeys } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { castRequestBody, useTypedApiClientBS } from "hooks/useTypedApiClientBS";
import { BaseReservationErrorDetails, BranchScheduleBlock, BranchScheduleBlockRequest } from "types/api-basereservation";

interface usePostScheduleBlockProps {
    onSuccess?: (
        data: BranchScheduleBlock,
        variables: BranchScheduleBlockRequest
    ) => void,
    onError?: (
        data: BaseReservationErrorDetails,
        variables: BranchScheduleBlockRequest
    ) => void,
    onSettled?: (
        data: BranchScheduleBlock | undefined,
        error: BaseReservationErrorDetails | null,
        variables: BranchScheduleBlockRequest
    ) => void
}

export const usePostScheduleBlock = ({
    onSuccess,
    onError,
    onSettled
}: usePostScheduleBlockProps) => {
    const path = `/api/BranchScheduleBlock`;
    const method = 'post';

    const postScheduleBlock = useTypedApiClientBS({ path, method })
    const queryClient = useQueryClient();

    const createScheduleBlocksMutation = useMutation({
        mutationKey: ['PostBranchScheduleBlock'],
        mutationFn: async (branchScheduleBlock: BranchScheduleBlockRequest) => {
            const { data } = await postScheduleBlock(castRequestBody(branchScheduleBlock, path, method))
            return data;
        },
        onSuccess: async (data: BranchScheduleBlock, variables: BranchScheduleBlockRequest) => {
            await queryClient.invalidateQueries({
                queryKey: ['GetScheduleBlocks', 'GetBranchSchedule']
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

    return createScheduleBlocksMutation;
}