import { isNil } from "lodash";
import { Button } from "@mui/material";
import { useEffect, useState } from "react";
import { getErrorMessage } from "utils/util";
import AddIcon from '@mui/icons-material/Add';
import { Page } from "components/Shared/Page";
import { ScheduleTable } from "./ScheduleTable";
import { useSnackbar } from "stores/useSnackbar";
import { PageHeader } from "components/Shared/PageHeader";
import { Link, useNavigate, useParams } from "react-router-dom";
import { useGetBranchById } from "hooks/api-basereservation/branch/useGetBranchById";
import { CircularLoadingProgress } from "components/LoadingProgress/CircularLoadingProcess";
import { ErrorProcess } from "components/Error/ErrorProcess";

export const Schedule = () => {
    const { branchId } = useParams<{ branchId?: string }>();
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
            setSnackbarMessage(`${getErrorMessage(error)}`, 'error')
            return;
        }
        setLoading(false)
    }, [isError, navigate, setSnackbarMessage, isValidBranchId, error]);

    if (isLoading || loading) {
        return <CircularLoadingProgress />
    }

    if (isError) {
        return <ErrorProcess />
    }

    return (
        <Page
            header={
                <PageHeader
                    title="Horarios sucursal"
                    subtitle={`${data?.name ?? ''}`}
                    backPath={`/Sucursal`}
                    backText="Sucursales"
                    actionButton={
                        <Link to={`/Sucursal/${Number(branchId)}/Horario/Gestion`}>
                            <Button variant="contained" size="large" fullWidth startIcon={<AddIcon />}>Gestión de horario</Button>
                        </Link>
                    } />
            }
        >
            <ScheduleTable branchId={Number(branchId)} schedules={data?.branchSchedules ?? []} />
        </Page>
    );
}