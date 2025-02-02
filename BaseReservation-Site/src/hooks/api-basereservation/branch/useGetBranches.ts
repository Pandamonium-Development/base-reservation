import { Branch } from "types/api-basereservation";
import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, useTypedApiClientBS } from "hooks/useTypedApiClientBS";

export const useGetBranches = (): UseQueryResult<Array<Branch>, ApiError> => {
    const path = '/api/Branch';
    const method = 'get';

    const getBranches = useTypedApiClientBS({ path, method })

    return useQuery({
        queryKey: ["GetBranches"],
        queryFn: async () => {
            const { data } = await getBranches(castRequestBody({}, path, method));
            return data
        },
        enabled: true
    })
}
