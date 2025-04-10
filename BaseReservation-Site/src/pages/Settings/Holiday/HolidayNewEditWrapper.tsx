import { isNil } from "lodash";
import { useEffect, useState } from "react";
import { getErrorMessage } from "utils/util";
import { useSnackbar } from "stores/useSnackbar";
import { HolidayNewEdit } from "./HolidayNewEdit";
import { useNavigate, useParams } from "react-router-dom";
import { useGetHolidayById } from "hooks/api-basereservation/holiday/useGetHolidayById";
import { CircularLoadingProgress } from "components/LoadingProgress/CircularLoadingProcess";

export const HolidayNewEditWrapper = () => {
    const { holidayId } = useParams<{ holidayId?: string }>();
    const navigate = useNavigate();
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    const { data, isLoading, isError, error } = useGetHolidayById(holidayId);

    const [loading, setLoading] = useState<boolean>(true);

    const isValidHolidayId = isNil(holidayId) || !isNil(holidayId) && !isNaN(Number(holidayId));

    useEffect(() => {
        if (!isValidHolidayId) {
            navigate('/General/Feriado');
            return;
        }
        if (isError) {
            navigate('/General/Feriado');
            setSnackbarMessage(`${getErrorMessage(error)}`, 'error')
            return;
        }
        setLoading(false)
    }, [isError, navigate, setSnackbarMessage, isValidHolidayId, error]);

    if (isLoading || loading) {
        return <CircularLoadingProgress />
    }

    return <HolidayNewEdit holidayData={data} />
}