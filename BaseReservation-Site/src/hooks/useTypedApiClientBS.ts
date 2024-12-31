import { paths } from "../api/base-reservation/api";
import { Fetcher, type TypedFetch } from "openapi-typescript-fetch";

export const useTypedApiClientBS = <
    PathT extends keyof paths,
    MethodT extends keyof paths[PathT]
>({
    path,
    method
}: {
    path: PathT
    method: MethodT
}): TypedFetch<paths[PathT][MethodT]> => {

    const fetcher = Fetcher.for<paths>();
    fetcher.configure({
        baseUrl: import.meta.env.VITE_API_BASERESERVATION_BASE_URL
    });

    return fetcher.path(path).method(method).create({}) as TypedFetch<paths[PathT][MethodT]> ;
}