import { transformErrorKeys } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBS } from "hooks/UseTypedApiClientBS";
import { BaseReservationErrorDetails, UnitMeasure, UnitMeasureRequest } from "types/api-basereservation";

interface UsePutUnitMeasureProps {
    onSuccess?: (
        data: UnitMeasure,
        variables: UnitMeasureRequest
    ) => void,
    onError?: (
        data: BaseReservationErrorDetails,
        variables: UnitMeasureRequest
    ) => void,
    onSettled?: (
        data: UnitMeasure | undefined,
        error: BaseReservationErrorDetails | null,
        variables: UnitMeasureRequest
    ) => void
}

export const UsePutUnitMeasure = ({
    onSuccess,
    onError,
    onSettled
}: UsePutUnitMeasureProps) => {
    const path = '/api/UnitMeasure/{unitMeasureId}';
    const method = 'put';

    const putUnitMeasure = UseTypedApiClientBS({ path, method })
    const queryClient = useQueryClient();

    const updateUnitMeasureMutation = useMutation({
        mutationKey: ['PutUnitMeasure'],
        mutationFn: async (unitMeasure: UnitMeasureRequest) => {
            const { data } = await putUnitMeasure(castRequestBody({ unitMeasureId: Number(unitMeasure.id), ...unitMeasure }, path, method));
            return data;
        },
        onSuccess: async (data: UnitMeasure, variables: UnitMeasureRequest) => {
            await queryClient.invalidateQueries({
                queryKey: ['GetUnitMeasure']
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

    return updateUnitMeasureMutation;
}