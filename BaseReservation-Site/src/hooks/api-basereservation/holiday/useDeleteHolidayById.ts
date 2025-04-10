import { transformErrorKeys } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { BaseReservationErrorDetails } from "types/api-basereservation";
import { castRequestBody, useTypedApiClientBS } from "hooks/useTypedApiClientBS";

interface useDeleteHolidayProps {
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

export const useDeleteHoliday = ({
    onSuccess,
    onError,
    onSettled
}: useDeleteHolidayProps) => {
    const path = '/api/Holiday/{holidayId}';
    const method = 'delete';

    const deleteHoliday = useTypedApiClientBS({ path, method })
    const queryClient = useQueryClient();

    const deleteHolidayMutation = useMutation({
        mutationKey: ['DeleteHoliday'],
        mutationFn: async (holidayId: number) => {
            const { data } = await deleteHoliday(castRequestBody({ holidayId }, path, method))
            return data;
        },
        onSuccess: async (data: boolean, variables: number) => {
            await queryClient.invalidateQueries({
                queryKey: ['Holidays']
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

    return deleteHolidayMutation;
}