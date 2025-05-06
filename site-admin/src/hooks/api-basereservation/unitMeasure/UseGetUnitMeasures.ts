import { UnitMeasure } from "types/api-basereservation";
import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBS } from "hooks/UseTypedApiClientBS";

export const UseGetUnitMeasures = (): UseQueryResult<Array<UnitMeasure>, ApiError> => {
    const path = '/api/UnitMeasure';
    const method = 'get';

    const getUnitMeasures = UseTypedApiClientBS({ path, method })

    return useQuery({
        queryKey: ["GetUnitMeasures"],
        queryFn: async () => {
            const { data } = await getUnitMeasures(castRequestBody({}, path, method));
            return data
        },
        enabled: true
    })
}
