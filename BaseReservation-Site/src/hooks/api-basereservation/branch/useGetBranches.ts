import { Branch } from "types/api-basereservation";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, useTypedApiClientBS } from "hooks/useTypedApiClientBS";

export const useGetBranches = (): UseQueryResult<Array<Branch>> => {
    const getBranches = useTypedApiClientBS({
        path: '/api/Branch',
        method: 'get'
    })

    return useQuery({
        queryKey: ["GetBranches"],
        queryFn: async () => {
            const { data } = await getBranches(castRequestBody({}, "/api/Branch", "get"));
            return data
        },
        enabled: true
    })
}
