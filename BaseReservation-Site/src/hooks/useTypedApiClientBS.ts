import Cookies from 'js-cookie';
import { paths } from "../api/base-reservation/api";
import { Fetcher, type TypedFetch } from "openapi-typescript-fetch";

const getHeaders = (disableAuth: boolean, token: string): Record<string, string> => {
    if (disableAuth) {
        return { "x-api-version": "1" }
    }

    return {
        "x-api-version": "1",
        Authorization: `Bearer ${token}`
    }
}

export const useTypedApiClientBS = <
    PathT extends keyof paths,
    MethodT extends keyof paths[PathT]
>({
    path,
    method,
    disableAuth = false
}: {
    path: PathT
    method: MethodT
    disableAuth?: boolean
}): TypedFetch<paths[PathT][MethodT]> => {
    const token = Cookies.get('access_token');
    const fetcher = Fetcher.for<paths>();
    fetcher.configure({
        baseUrl: import.meta.env.VITE_API_BASERESERVATION_BASE_URL,
        init: {
            headers: getHeaders(disableAuth, token ?? ''),
        },
    });

    return fetcher.path(path).method(method).create({}) as TypedFetch<paths[PathT][MethodT]>;
}