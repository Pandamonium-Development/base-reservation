import { transformErrorKeys } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBS } from "hooks/UseTypedApiClientBS";
import { BaseReservationErrorDetails, Branch, BranchRequest } from "types/api-basereservation";

interface UsePostBranchProps {
    onSuccess?: (
        data: Branch,
        variables: BranchRequest
    ) => void,
    onError?: (
        data: BaseReservationErrorDetails,
        variables: BranchRequest
    ) => void,
    onSettled?: (
        data: Branch | undefined,
        error: BaseReservationErrorDetails | null,
        variables: BranchRequest
    ) => void
}

export const UsePostBranch = ({
    onSuccess,
    onError,
    onSettled
}: UsePostBranchProps) => {
    const path = '/api/Branch';
    const method = 'post';

    const postBranch = UseTypedApiClientBS({ path, method })
    const queryClient = useQueryClient();

    const createBranchMutation = useMutation({
        mutationKey: ['PostBranch'],
        mutationFn: async (branch: BranchRequest) => {
            const { data } = await postBranch(castRequestBody(branch, path, method))
            return data;
        },
        onSuccess: async (data: Branch, variables: BranchRequest) => {
            await queryClient.invalidateQueries({
                queryKey: ['GetBranch']
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

    return createBranchMutation;
}