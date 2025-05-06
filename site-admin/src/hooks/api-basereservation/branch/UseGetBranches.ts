import { Branch } from "types/api-basereservation";
import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBS } from "hooks/UseTypedApiClientBS";

export const UseGetBranches = (): UseQueryResult<Array<Branch>, ApiError> => {
    const path = '/api/Branch';
    const method = 'get';

    const getBranches = UseTypedApiClientBS({ path, method })

    return useQuery({
        queryKey: ["GetBranches"],
        queryFn: async () => {
            const { data } = await getBranches(castRequestBody({}, path, method));
            return data
        },
        enabled: true,
        staleTime: 0,
    })
}
