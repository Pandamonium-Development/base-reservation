import { transformErrorKeys } from "utils/util"
import { useMutation } from "@tanstack/react-query"
import { ApiError } from "openapi-typescript-fetch"
import { castRequestBody, UseTypedApiClientBS } from "hooks/UseTypedApiClientBS"
import { Authentication, BaseReservationErrorDetails, LoginUserRequest } from "types/api-basereservation"

interface UsePostAuthenticationProps {
    onSuccess?: (
        data: Authentication,
        variables: LoginUserRequest
    ) => void,
    onError?: (
        data: BaseReservationErrorDetails,
        variables: LoginUserRequest
    ) => void
}

export const UsePostAuthentication = ({
    onSuccess,
    onError
}: UsePostAuthenticationProps) => {
    const path = '/api/Authentication';
    const method = 'post';

    const postAuthentication = UseTypedApiClientBS({ path, method })

    return useMutation({
        mutationFn: async (
            loginUserInformation: LoginUserRequest
        ) => {
            const { data } = await postAuthentication(castRequestBody(loginUserInformation, path, method))
            return data;
        },
        onSuccess,
        onError: (error: ApiError, _) => {
            onError?.(transformErrorKeys(error.data) as BaseReservationErrorDetails, _)
        }
    })
}