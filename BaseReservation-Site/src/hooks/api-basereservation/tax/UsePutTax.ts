import { transformErrorKeys } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBS } from "hooks/UseTypedApiClientBS";
import { BaseReservationErrorDetails, Tax, TaxRequest } from "types/api-basereservation";

interface UsePutTaxProps {
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

export const UsePutTax = ({
    onSuccess,
    onError,
    onSettled
}: UsePutTaxProps) => {
    const path = '/api/Tax/{taxId}';
    const method = 'put';

    const putTax = UseTypedApiClientBS({ path, method })
    const queryClient = useQueryClient();

    const updateTaxMutation = useMutation({
        mutationKey: ['PutTax'],
        mutationFn: async (tax: TaxRequest) => {
            const { data } = await putTax(castRequestBody({ taxId: Number(tax.id), ...tax }, path, method));
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

    return updateTaxMutation;
}