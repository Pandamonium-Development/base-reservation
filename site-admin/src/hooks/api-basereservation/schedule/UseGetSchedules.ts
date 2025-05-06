import { ApiError } from "openapi-typescript-fetch";
import { Schedule } from "types/api-basereservation";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBS } from "hooks/UseTypedApiClientBS";

export const UseGetSchedules = (): UseQueryResult<Array<Schedule>, ApiError> => {
    const path = '/api/Schedule';
    const method = 'get';

    const getSchedules = UseTypedApiClientBS({ path, method })

    return useQuery({
        queryKey: ["GetSchedules"],
        queryFn: async () => {
            const { data } = await getSchedules(castRequestBody({}, path, method));
            return data
        },
        enabled: true
    })
}
