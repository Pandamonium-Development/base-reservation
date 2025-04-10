import { useEffect, useState } from "react";
import { getErrorMessage } from "utils/util";
import { BranchNewEdit } from "./BranchNewEdit";
import { useSnackbar } from "stores/useSnackbar";
import { useNavigate, useParams } from "react-router-dom";
import { UseGetBranchById } from "hooks/api-basereservation/branch/UseGetBranchById";
import { CircularLoadingProgress } from "components/LoadingProgress/CircularLoadingProcess";

export const BranchNewEditWrapper = () => {
    const { branchId } = useParams<{ branchId?: string }>();
    const navigate = useNavigate();
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    const { data, isLoading, isError, error } = UseGetBranchById(branchId);

    const [loading, setLoading] = useState<boolean>(true);

    const isValidBranchId = (branchId ?? '') !== '' && !isNaN(Number(branchId));

    useEffect(() => {
        if (!isValidBranchId) {
            navigate('/Sucursal');
            return;
        }
        if (isError) {
            navigate('/Sucursal');
            setSnackbarMessage(`${getErrorMessage(error)}`, 'error')
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