import { transformErrorKeys } from "utils/util"
import { useMutation } from "@tanstack/react-query"
import { ApiError } from "openapi-typescript-fetch"
import { castRequestBody, useTypedApiClientBS } from "hooks/useTypedApiClientBS"
import { Authentication, BaseReservationErrorDetails, LoginUserRequest } from "types/api-basereservation"

interface usePostAuthenticationProps {
    onSuccess?: (
        data: Authentication,
        variables: LoginUserRequest
    ) => void,
    onError?: (
        data: BaseReservationErrorDetails,
        variables: LoginUserRequest
    ) => void
}

export const usePostAuthentication = ({
    onSuccess,
    onError
}: usePostAuthenticationProps) => {
    const postAuthentication = useTypedApiClientBS({
        path: '/api/Authentication',
        method: 'post'
    })

    return useMutation({
        mutationFn: async (
            loginUserInformation: LoginUserRequest
        ) => {
            //const { data } = await postAuthentication(loginUserInformation as OpArgType<paths['/api/Authentication']['post']['requestBody']> as never);
            const { data } = await postAuthentication(castRequestBody(loginUserInformation, "/api/Authentication", "post"))
            return data;
        },
        onSuccess,
        onError: (error: ApiError, _) => {
            onError?.(transformErrorKeys(error.data) as BaseReservationErrorDetails, _)
        }
    })
}