import { transformErrorKeys } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { BaseReservationErrorDetails } from "types/api-basereservation";
import { castRequestBody, UseTypedApiClientBS } from "hooks/UseTypedApiClientBS";

interface UseDeleteTaxProps {
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

export const UseDeleteTax = ({
    onSuccess,
    onError,
    onSettled
}: UseDeleteTaxProps) => {
    const path = '/api/Tax/{taxId}';
    const method = 'delete';

    const deleteTax = UseTypedApiClientBS({ path, method })
    const queryClient = useQueryClient();

    const deleteTaxMutation = useMutation({
        mutationKey: ['DeleteTax'],
        mutationFn: async (taxId: number) => {
            const { data } = await deleteTax(castRequestBody({ taxId }, path, method))
            return data;
        },
        onSuccess: async (data: boolean, variables: number) => {
            await queryClient.invalidateQueries({
                queryKey: ['GetTaxes']
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

    return deleteTaxMutation;
}