import { transformErrorKeys } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { castRequestBody, useTypedApiClientBS } from "hooks/useTypedApiClientBS";
import { BaseReservationErrorDetails, Holiday, HolidayRequest } from "types/api-basereservation";

interface usePostHolidayProps {
    onSuccess?: (
        data: Holiday,
        variables: HolidayRequest
    ) => void,
    onError?: (
        data: BaseReservationErrorDetails,
        variables: HolidayRequest
    ) => void,
    onSettled?: (
        data: Holiday | undefined,
        error: BaseReservationErrorDetails | null,
        variables: HolidayRequest
    ) => void
}

export const usePostHoliday = ({
    onSuccess,
    onError,
    onSettled
}: usePostHolidayProps) => {
    const path = '/api/Holiday';
    const method = 'post';

    const postHoliday = useTypedApiClientBS({ path, method })
    const queryClient = useQueryClient();

    const createHolidayMutation = useMutation({
        mutationKey: ['PostHoliday'],
        mutationFn: async (holiday: HolidayRequest) => {
            const { data } = await postHoliday(castRequestBody(holiday, path, method))
            return data;
        },
        onSuccess: async (data: Holiday, variables: HolidayRequest) => {
            await queryClient.invalidateQueries({
                queryKey: ['GetHoliday']
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

    return createHolidayMutation;
}