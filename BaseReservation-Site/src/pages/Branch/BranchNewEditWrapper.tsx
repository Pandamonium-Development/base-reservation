import { isNil } from "lodash";
import { useEffect, useState } from "react";
import { BranchNewEdit } from "./BranchNewEdit";
import { useSnackbar } from "stores/useSnackbar";
import { useNavigate, useParams } from "react-router-dom";
import { useGetBranchById } from "hooks/api-basereservation/branch/useGetBranchById";
import { CircularLoadingProgress } from "components/LoadingProgress/CircularLoadingProcess";
import { BaseReservationErrorDetails } from "types/api-basereservation";
import { transformErrorKeys } from "utils/util";
import { ApiError } from "openapi-typescript-fetch";

const getErrorMessage = (error: ApiError) => {
    const errorDetail = transformErrorKeys(error.data as BaseReservationErrorDetails);
    return errorDetail.message;
}

export const BranchNewEditWrapper = () => {
    const { id: branchId } = useParams<{ id?: string }>();
    const navigate = useNavigate();
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    const { data, isLoading, isError, error } = useGetBranchById(branchId);

    const [loading, setLoading] = useState<boolean>(true);

    const isValidBranchId = isNil(branchId) || !isNil(branchId) && !isNaN(Number(branchId));

    useEffect(() => {
        if (!isValidBranchId) {
            navigate('/Sucursal');
            return;
        }
        if (isError) {
            navigate('/Sucursal');
            setSnackbarMessage(`${getErrorMessage(error as ApiError)}`, 'error')
            return;
        }
        setLoading(false)
    }, [isError, navigate, setSnackbarMessage, isValidBranchId, error]);

    if (isLoading || loading) {
        return <CircularLoadingProgress />
    }

    return (
        <BranchNewEdit branchData={data} />
    );

}