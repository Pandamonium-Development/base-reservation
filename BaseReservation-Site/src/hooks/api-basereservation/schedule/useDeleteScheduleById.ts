import { transformErrorKeys } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { BaseReservationErrorDetails } from "types/api-basereservation";
import { castRequestBody, useTypedApiClientBS } from "hooks/useTypedApiClientBS";

interface useDeleteScheduleProps {
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

export const useDeleteSchedule = ({
    onSuccess,
    onError,
    onSettled
}: useDeleteScheduleProps) => {
    const path = '/api/Schedule/{scheduleId}';
    const method = 'delete';

    const deleteSchedule = useTypedApiClientBS({ path, method })
    const queryClient = useQueryClient();

    const deleteScheduleMutation = useMutation({
        mutationKey: ['DeleteSchedule'],
        mutationFn: async (scheduleId: number) => {
            const { data } = await deleteSchedule(castRequestBody({ scheduleId }, path, method))
            return data;
        },
        onSuccess: async (data: boolean, variables: number) => {
            await queryClient.invalidateQueries({
                queryKey: ['Schedules']
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

    return deleteScheduleMutation;
}