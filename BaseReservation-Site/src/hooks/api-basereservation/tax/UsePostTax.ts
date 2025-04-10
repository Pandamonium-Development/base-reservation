import { transformErrorKeys } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBS } from "hooks/UseTypedApiClientBS";
import { BaseReservationErrorDetails, Tax, TaxRequest } from "types/api-basereservation";

interface UsePostTaxProps {
    onSuccess?: (
        data: Tax,
        variables: TaxRequest
    ) => void,
    onError?: (
        data: BaseReservationErrorDetails,
        variables: TaxRequest
    ) => void,
    onSettled?: (
        data: Tax | undefined,
        error: BaseReservationErrorDetails | null,
        variables: TaxRequest
    ) => void
}

export const UsePostTax = ({
    onSuccess,
    onError,
    onSettled
}: UsePostTaxProps) => {
    const path = '/api/Tax';
    const method = 'post';

    const postTax = UseTypedApiClientBS({ path, method })
    const queryClient = useQueryClient();

    const createTaxMutation = useMutation({
        mutationKey: ['PostTax'],
        mutationFn: async (tax: TaxRequest) => {
            const { data } = await postTax(castRequestBody(tax, path, method))
            return data;
        },
        onSuccess: async (data: Tax, variables: TaxRequest) => {
            await queryClient.invalidateQueries({
                queryKey: ['GetTax']
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

    return createTaxMutation;
}