import { transformErrorKeys } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { BaseReservationErrorDetails } from "types/api-basereservation";
import { castRequestBody, useTypedApiClientBS } from "hooks/useTypedApiClientBS";

interface useDeleteBranch {
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

export const useDeleteBranch = ({
    onSuccess,
    onError,
    onSettled
}: useDeleteBranch) => {
    const path = '/api/Branch/{branchId}';
    const method = 'delete';

    const deleteBranch = useTypedApiClientBS({ path, method })
    const queryClient = useQueryClient();

    const deleteBranchMutation = useMutation({
        mutationKey: ['DeleteBranch'],
        mutationFn: async (branchId: number) => {
            const { data } = await deleteBranch(castRequestBody({ branchId }, path, method))
            return data;
        },
        onSuccess: async (data: boolean, variables: number) => {
            await queryClient.invalidateQueries({
                queryKey: ['Branches']
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

    return deleteBranchMutation;
}