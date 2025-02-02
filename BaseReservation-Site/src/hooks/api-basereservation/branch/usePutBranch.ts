import { transformErrorKeys } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { castRequestBody, useTypedApiClientBS } from "hooks/useTypedApiClientBS";
import { BaseReservationErrorDetails, Branch, BranchRequest } from "types/api-basereservation";

interface usePutBranchProps {
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

export const usePutBranch = ({
    onSuccess,
    onError,
    onSettled
}: usePutBranchProps) => {
    const path = '/api/Branch/{branchId}';
    const method = 'put';

    const putBranch = useTypedApiClientBS({ path, method })
    const queryClient = useQueryClient();

    const updateBranchMutation = useMutation({
        mutationKey: ['PutBranch'],
        mutationFn: async (branch: BranchRequest) => {
            const { data } = await putBranch(castRequestBody({ branchId: Number(branch.id), ...branch }, path, method));
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

    return updateBranchMutation;
}