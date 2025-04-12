import { transformErrorKeys } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { BaseReservationErrorDetails } from "types/api-basereservation";
import { castRequestBody, UseTypedApiClientBS } from "hooks/UseTypedApiClientBS";

interface UseDeleteUnitMeasureProps {
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

export const UseDeleteUnitMeasure = ({
    onSuccess,
    onError,
    onSettled
}: UseDeleteUnitMeasureProps) => {
    const path = '/api/UnitMeasure/{unitMeasureId}';
    const method = 'delete';

    const deleteUnitMeasure = UseTypedApiClientBS({ path, method })
    const queryClient = useQueryClient();

    const deleteUnitMeasureMutation = useMutation({
        mutationKey: ['DeleteUnitMeasure'],
        mutationFn: async (unitMeasureId: number) => {
            const { data } = await deleteUnitMeasure(castRequestBody({ unitMeasureId }, path, method))
            return data;
        },
        onSuccess: async (data: boolean, variables: number) => {
            await queryClient.invalidateQueries({
                queryKey: ['GetUnitMeasures']
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

    return deleteUnitMeasureMutation;
}