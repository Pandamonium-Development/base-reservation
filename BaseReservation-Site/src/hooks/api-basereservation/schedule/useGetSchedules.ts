import { ApiError } from "openapi-typescript-fetch";
import { Schedule } from "types/api-basereservation";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, useTypedApiClientBS } from "hooks/useTypedApiClientBS";

export const useGetSchedules = (): UseQueryResult<Array<Schedule>, ApiError> => {
    const path = '/api/Schedule';
    const method = 'get';

    const getSchedules = useTypedApiClientBS({ path, method })

    return useQuery({
        queryKey: ["GetSchedules"],
        queryFn: async () => {
            const { data } = await getSchedules(castRequestBody({}, path, method));
            return data
        },
        enabled: true
    })
}
