import { ApiError } from "openapi-typescript-fetch";
import { Schedule } from "types/api-basereservation";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBS } from "hooks/UseTypedApiClientBS";

export const UseGetHolidays = (): UseQueryResult<Array<Schedule>, ApiError> => {
    const path = '/api/Holiday';
    const method = 'get';

    const getHolidays = UseTypedApiClientBS({ path, method })

    return useQuery({
        queryKey: ["GetHolidays"],
        queryFn: async () => {
            const { data } = await getHolidays(castRequestBody({}, path, method));
            return data
        },
        enabled: true
    })
}
