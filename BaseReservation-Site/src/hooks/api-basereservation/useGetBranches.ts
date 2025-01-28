import { Branch } from "types/api-basereservation";
import { useTypedApiClientBS } from "../useTypedApiClientBS";
import { useQuery, UseQueryResult } from "@tanstack/react-query";

export const useGetBranches = (): UseQueryResult<Array<Branch>> => {
    const getBranches = useTypedApiClientBS({
        path: '/api/Branch',
        method: 'get'
    })

    return useQuery({
        queryKey: ["GetBranches"],
        queryFn: async () => {
            const { data } = await getBranches({});
            return data
        },
        enabled: true
    })
}
