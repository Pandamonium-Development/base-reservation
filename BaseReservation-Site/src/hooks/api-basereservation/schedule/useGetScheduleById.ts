import { isPresent } from "utils/util";
import { Schedule } from "types/api-basereservation";
import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBS } from "hooks/UseTypedApiClientBS";

export const UseGetScheduleById = (scheduleId: string | undefined): UseQueryResult<Schedule, ApiError> => {
    const path = '/api/Schedule/{scheduleId}';
    const method = 'get';

    const getSchedule = UseTypedApiClientBS({ path, method })

    return useQuery({
        queryKey: ["GetSchedule", scheduleId],
        queryFn: async () => {
            const { data } = await getSchedule(castRequestBody({ scheduleId: Number(scheduleId) }, path, method));
            return data
        },
        retry: false,
        enabled: isPresent(scheduleId),
        staleTime: 0,
    })
}
