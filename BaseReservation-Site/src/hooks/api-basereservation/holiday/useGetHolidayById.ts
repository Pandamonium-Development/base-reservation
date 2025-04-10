import { isPresent } from "utils/util";
import { Holiday } from "types/api-basereservation";
import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBS } from "hooks/UseTypedApiClientBS";

export const UseGetHolidayById = (holidayId: string | undefined): UseQueryResult<Holiday, ApiError> => {
    const path = '/api/Holiday/{holidayId}';
    const method = 'get';

    const getHoliday = UseTypedApiClientBS({ path, method })

    return useQuery({
        queryKey: ["GetHoliday", holidayId],
        queryFn: async () => {
            const { data } = await getHoliday(castRequestBody({ holidayId: Number(holidayId) }, path, method));
            return data
        },
        retry: false,
        enabled: isPresent(holidayId),
        staleTime: 0,
    })
}
