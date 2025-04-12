import { isPresent } from "utils/util";
import { UnitMeasure } from "types/api-basereservation";
import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBS } from "hooks/UseTypedApiClientBS";

export const UseGetUnitMeasureById = (unitMeasureId: string | undefined): UseQueryResult<UnitMeasure, ApiError> => {
    const path = '/api/UnitMeasure/{unitMeasureId}';
    const method = 'get';

    const getUnitMeasure = UseTypedApiClientBS({ path, method })

    return useQuery({
        queryKey: ["GetUnitMeasure", unitMeasureId],
        queryFn: async () => {
            const { data } = await getUnitMeasure(castRequestBody({ unitMeasureId: Number(unitMeasureId) }, path, method));
            return data
        },
        retry: false,
        enabled: isPresent(unitMeasureId),
        staleTime: 0,
    })
}
