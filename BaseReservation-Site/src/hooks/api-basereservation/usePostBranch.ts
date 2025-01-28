import { transformErrorKeys } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";
import { useTypedApiClientBS } from "hooks/useTypedApiClientBS";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { BaseReservationErrorDetails, Branch, BranchRequest } from "types/api-basereservation";

interface usePostBranchProps {
    onSuccess?: (
        data: Branch,
        variables: BranchRequest
    ) => void,
    onError?: (
        data: BaseReservationErrorDetails,
        variables: BranchRequest
    ) => void
}

export const usePostBranch = ({
    onSuccess,
    onError
}: usePostBranchProps) => {
    const postBranch = useTypedApiClientBS({
        path: '/api/Branch',
        method: 'post'
    })
    const queryClient = useQueryClient();

    const createBranch = useMutation({
        mutationKey: ['PostBranch'],
        mutationFn: async (data: BranchRequest) => {
            const response = await postBranch({
                name: data.name,
                description: data.description,
                telephone: data.telephone,
                email: data.email,
                districtId: data.districtId,
                address: data.address,
                active: data.active
            })
            return response.data;
        },
        onSuccess: async (data: Branch, variables: BranchRequest) => {
            await queryClient.invalidateQueries({
                queryKey: ['Branches']
            })
            onSuccess?.(data, variables)
        },
        onError: (error: ApiError, _) => {
            onError?.(transformErrorKeys(error.data) as BaseReservationErrorDetails, _)
        }
    })

    return createBranch;
}