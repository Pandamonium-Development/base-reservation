import { isNil } from "lodash";
import { useEffect, useState } from "react";
import { BranchNewEdit } from "./BranchNewEdit";
import { useSnackbar } from "stores/useSnackbar";
import { useNavigate, useParams } from "react-router-dom";
import { useGetBranchById } from "hooks/api-basereservation/branch/useGetBranchById";
import { CircularLoadingProgress } from "components/LoadingProgress/CircularLoadingProcess";

export const BranchNewEditWrapper = () => {
    const { id: branchId } = useParams<{ id?: string }>();
    const navigate = useNavigate();
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    const { data, isLoading, isError } = useGetBranchById(branchId);

    const [loading, setLoading] = useState<boolean>(true);

    const isValidBranchId = isNil(branchId) || !isNil(branchId) && !isNaN(Number(branchId));

    useEffect(() => {
        if (!isValidBranchId) {
            navigate('/Sucursal');
            return;
        }
        setLoading(false)
    }, [isError, navigate, setSnackbarMessage, isValidBranchId]);

    if (isLoading || loading) {
        return <CircularLoadingProgress />
    }

    return (
        <BranchNewEdit branchData={data} />
    );

}