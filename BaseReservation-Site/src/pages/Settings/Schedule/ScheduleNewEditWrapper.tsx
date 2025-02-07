import { useSnackbar } from "stores/useSnackbar";
import { useNavigate, useParams } from "react-router-dom";
import { useGetScheduleById } from "hooks/api-basereservation/schedule/useGetScheduleById";
import { useEffect, useState } from "react";
import { isNil } from "lodash";
import { getErrorMessage } from "utils/util";
import { CircularLoadingProgress } from "components/LoadingProgress/CircularLoadingProcess";
import { ScheduleNewEdit } from "./ScheduleNewEdit";

export const ScheduleNewEditWrapper = () => {
    const { scheduleId } = useParams<{ scheduleId?: string }>();
    const navigate = useNavigate();
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    const { data, isLoading, isError, error } = useGetScheduleById(scheduleId);

    const [loading, setLoading] = useState<boolean>(true);

    const isValidBranchId = isNil(scheduleId) || !isNil(scheduleId) && !isNaN(Number(scheduleId));

    useEffect(() => {
        if (!isValidBranchId) {
            navigate('/General/Horario');
            return;
        }
        if (isError) {
            navigate('/General/Horario');
            setSnackbarMessage(`${getErrorMessage(error)}`, 'error')
            return;
        }
        setLoading(false)
    }, [isError, navigate, setSnackbarMessage, isValidBranchId, error]);

    if (isLoading || loading) {
        return <CircularLoadingProgress />
    }

    return <ScheduleNewEdit scheduleData={data} />
}