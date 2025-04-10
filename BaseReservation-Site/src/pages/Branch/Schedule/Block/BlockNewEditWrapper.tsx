import { isNil } from "lodash";
import { useEffect, useState } from "react";
import { getErrorMessage } from "utils/util";
import { BlockNewEdit } from "./BlockNewEdit";
import { useSnackbar } from "stores/useSnackbar";
import { useNavigate, useParams } from "react-router-dom";
import { CircularLoadingProgress } from "components/LoadingProgress/CircularLoadingProcess";
import { UseGetScheduleBlockById } from "hooks/api-basereservation/branch/schedule/block/UseGetScheduleBlockById";

export const BlockNewEditWrapper = () => {
    const { branchId, scheduleId, blockId } = useParams<{ branchId?: string, scheduleId?: string, blockId?: string }>();
    const navigate = useNavigate();
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    const { data, isLoading, isError, error } = UseGetScheduleBlockById(blockId);

    const [loading, setLoading] = useState<boolean>(true);

    const isValidBranchId = isNil(branchId) || !isNil(branchId) && !isNaN(Number(branchId));
    const isValidScheduleId = isNil(scheduleId) || !isNil(scheduleId) && !isNaN(Number(scheduleId));
    const isValidBlockId = isNil(blockId) || !isNil(blockId) && !isNaN(Number(blockId));

    if ((!isValidBranchId || !isValidScheduleId) && !isLoading && !isError && !loading && (branchId != data?.branchSchedule?.branchId || scheduleId != data?.branchScheduleId)) {
        setSnackbarMessage("Horario o sucursal no son válidos con el bloque", "error")
        navigate(`/Sucursal/${branchId}/Horario/${scheduleId}/Bloqueo`);
    }

    useEffect(() => {
        if (!isValidBranchId || !isValidScheduleId || (!isNil(isValidBlockId) && !isValidBlockId)) {
            navigate(`/Sucursal/${branchId}/Horario/${scheduleId}/Bloqueo`);
            return;
        }
        if (isError) {
            navigate(`/Sucursal/${branchId}/Horario/${scheduleId}/Bloqueo`);
            setSnackbarMessage(`${getErrorMessage(error)}`, 'error')
            return;
        }
        setLoading(false)
    }, [isError, navigate, setSnackbarMessage, isValidBranchId, isValidScheduleId, isValidBlockId, branchId, scheduleId, error]);

    if (isLoading || loading) {
        return <CircularLoadingProgress />
    }

    return (
        <BlockNewEdit branchScheduleBlockData={data} />
    )
}