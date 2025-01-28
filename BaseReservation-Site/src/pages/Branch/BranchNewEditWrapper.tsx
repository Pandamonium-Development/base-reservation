import { isNil } from "lodash";
import { useEffect, useState } from "react";
import { BranchNewEdit } from "./BranchNewEdit";
import { useSnackbar } from "stores/useSnackbar";
import { Box, CircularProgress } from "@mui/material";
import { useNavigate, useParams } from "react-router-dom";
import { useGetBranchById } from "hooks/api-basereservation/useGetBranchById";

export const BranchNewEditWrapper = () => {
    const { id: branchId } = useParams<{ id?: string }>();
    const navigate = useNavigate();
    const setMessage = useSnackbar((state) => state.setMessage);

    const { data, isLoading, isError } = useGetBranchById(branchId);
    const [loading, setLoading] = useState<boolean>(true);

    const isValidBranchId = isNil(branchId) || !isNil(branchId) && !isNaN(Number(branchId));

    useEffect(() => {
        if (!isValidBranchId) {
            navigate('/Sucursal');
            return;
        }
        setLoading(false)
    }, [isError, navigate, setMessage, isValidBranchId]);

    if (isLoading || loading) {
        return (
            <Box display="flex" justifyContent="center" alignItems="center" height="100vh">
                <CircularProgress />
            </Box>
        );
    }

    return (
        <BranchNewEdit branchData={data} />
    );
}