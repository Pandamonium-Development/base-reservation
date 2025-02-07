import { transformErrorKeys } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { BaseReservationErrorDetails } from "types/api-basereservation";
import { castRequestBody, useTypedApiClientBS } from "hooks/useTypedApiClientBS";

interface useDeleteScheduleBlockProps {
    onSuccess?: (
        data: boolean,
        variables: number
    ) => void,
    onError?: (
        data: BaseReservationErrorDetails,
        variables: number
    ) => void,
    onSettled?: (
        data: boolean | undefined,
        error: BaseReservationErrorDetails | null,
        variables: number
    ) => void
}

export const useDeleteScheduleBlock = ({
    onSuccess,
    onError,
    onSettled
}: useDeleteScheduleBlockProps) => {
    const path = '/api/BranchScheduleBlock/{blockId}';
    const method = 'delete';

    const deleteBranchScheduleBlcok = useTypedApiClientBS({ path, method })
    const queryClient = useQueryClient();

    const deleteBranchScheduleBlockMutation = useMutation({
        mutationKey: ['DeleteBranch'],
        mutationFn: async (blockId: number) => {
            const { data } = await deleteBranchScheduleBlcok(castRequestBody({ blockId }, path, method))
            return data;
        },
        onSuccess: async (data: boolean, variables: number) => {
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

    return deleteBranchScheduleBlockMutation;
}