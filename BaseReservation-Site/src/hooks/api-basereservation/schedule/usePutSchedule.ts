import { transformErrorKeys } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { castRequestBody, useTypedApiClientBS } from "hooks/useTypedApiClientBS";
import { BaseReservationErrorDetails, Schedule, ScheduleRequest } from "types/api-basereservation";

interface usePutScheduleProps {
    onSuccess?: (
        data: Schedule,
        variables: ScheduleRequest
    ) => void,
    onError?: (
        data: BaseReservationErrorDetails,
        variables: ScheduleRequest
    ) => void,
    onSettled?: (
        data: Schedule | undefined,
        error: BaseReservationErrorDetails | null,
        variables: ScheduleRequest
    ) => void
}

export const usePutSchedule = ({
    onSuccess,
    onError,
    onSettled
}: usePutScheduleProps) => {
    const path = '/api/Schedule/{scheduleId}';
    const method = 'put';

    const putSchedule = useTypedApiClientBS({ path, method })
    const queryClient = useQueryClient();

    const updateScheduleMutation = useMutation({
        mutationKey: ['PutSchedule'],
        mutationFn: async (schedule: ScheduleRequest) => {
            const { data } = await putSchedule(castRequestBody({ scheduleId: Number(schedule.id), ...schedule }, path, method));
            return data;
        },
        onSuccess: async (data: Schedule, variables: ScheduleRequest) => {
            await queryClient.invalidateQueries({
                queryKey: ['GetSchedule']
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

    return updateScheduleMutation;
}