import { ApiError } from "openapi-typescript-fetch";
import { Province } from "types/api-basereservation";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBS } from "../UseTypedApiClientBS";

export const UseGetProvinces = (): UseQueryResult<Array<Province>, ApiError> => {
    const path = '/api/Province';
    const method = 'get';

    const getProvinces = UseTypedApiClientBS({ path, method })

    return useQuery({
        queryKey: ["Provinces"],
        queryFn: async () => {
            const { data } = await getProvinces(castRequestBody({}, path, method));
            return data
        },
        enabled: true
    })
}
