import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { useTypedApiClientBS } from "../useTypedApiClientBS";
import { Branch } from "types/api-basereservation";

export const useGetBranches = (): UseQueryResult<Array<Branch>> => {
    const getBranches = useTypedApiClientBS({
        path: '/api/Branch',
        method: 'get'
    })

    return useQuery({
        queryKey: ["Branches"],
        queryFn: async () => {
            const { data } = await getBranches({});
            return data
        },
        enabled: true
    })
}
