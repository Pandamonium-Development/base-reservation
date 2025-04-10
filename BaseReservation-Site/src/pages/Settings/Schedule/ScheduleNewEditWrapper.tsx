import { isNil } from "lodash";
import { useEffect, useState } from "react";
import { getErrorMessage } from "utils/util";
import { useSnackbar } from "stores/useSnackbar";
import { ScheduleNewEdit } from "./ScheduleNewEdit";
import { useNavigate, useParams } from "react-router-dom";
import { CircularLoadingProgress } from "components/LoadingProgress/CircularLoadingProcess";
import { UseGetScheduleById } from "hooks/api-basereservation/schedule/UseGetScheduleById";

export const ScheduleNewEditWrapper = () => {
    const { scheduleId } = useParams<{ scheduleId?: string }>();
    const navigate = useNavigate();
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    const { data, isLoading, isError, error } = UseGetScheduleById(scheduleId);

    const [loading, setLoading] = useState<boolean>(true);

    const isValidScheduleId = isNil(scheduleId) || !isNil(scheduleId) && !isNaN(Number(scheduleId));

    useEffect(() => {
        if (!isValidScheduleId) {
            navigate('/General/Horario');
            return;
        }
        if (isError) {
            navigate('/General/Horario');
            setSnackbarMessage(`${getErrorMessage(error)}`, 'error')
            return;
        }
        setLoading(false)
    }, [isError, navigate, setSnackbarMessage, isValidScheduleId, error]);

    if (isLoading || loading) {
        return <CircularLoadingProgress />
    }

    return <ScheduleNewEdit scheduleData={data} />
}