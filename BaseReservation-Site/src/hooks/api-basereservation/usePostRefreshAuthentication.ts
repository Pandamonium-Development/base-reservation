import { transformErrorKeys } from "utils/util"
import { useMutation } from "@tanstack/react-query"
import { ApiError } from "openapi-typescript-fetch"
import { useTypedApiClientBS } from "hooks/useTypedApiClientBS"
import { Authentication, BaseReservationErrorDetails, UserTokenRefreshRequest } from "types/api-basereservation"

interface usePostRefreshAuthenticationProps {
    onSuccess?: (
        data: Authentication,
        variables: UserTokenRefreshRequest
    ) => void,
    onError?: (
        data: BaseReservationErrorDetails,
        variables: UserTokenRefreshRequest
    ) => void
}

export const usePostRefreshAuthentication = ({
    onSuccess,
    onError
}: usePostRefreshAuthenticationProps) => {
    const postRefreshToken = useTypedApiClientBS({
        path: '/api/Authentication/refreshToken',
        method: 'post'
    })

    return useMutation({
        mutationFn: async (
            tokenRefreshModel: UserTokenRefreshRequest
        ) => {
            const { data } = await postRefreshToken(tokenRefreshModel);
            return data;
        },
        onSuccess,
        onError: (error: ApiError, _) => {
            onError?.(transformErrorKeys(error.data) as BaseReservationErrorDetails, _)
        }
    })
}