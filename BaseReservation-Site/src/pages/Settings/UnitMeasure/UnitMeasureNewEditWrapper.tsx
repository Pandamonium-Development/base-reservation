import { isNil } from "lodash";
import { useEffect, useState } from "react";
import { getErrorMessage } from "utils/util";
import { useSnackbar } from "stores/useSnackbar";
import { useNavigate, useParams } from "react-router-dom";
import { UnitMeasureNewEdit } from "./UnitMeasureNewEdit";
import { CircularLoadingProgress } from "components/LoadingProgress/CircularLoadingProcess";
import { UseGetUnitMeasureById } from "hooks/api-basereservation/unitMeasure/UseGetUnitMeasureById";


export const UnitMeasureNewEditWrapper = () => {
    const { unitMeasureId } = useParams<{ unitMeasureId?: string }>();
    const navigate = useNavigate();
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    const { data, isLoading, isError, error } = UseGetUnitMeasureById(unitMeasureId);

    const [loading, setLoading] = useState<boolean>(true);

    const isValidUnitMeasureId = isNil(unitMeasureId) || !isNil(unitMeasureId) && !isNaN(Number(unitMeasureId));

    useEffect(() => {
        if (!isValidUnitMeasureId) {
            navigate('/General/UnidadMedida');
            return;
        }
        if (isError) {
            navigate('/General/UnidadMedida');
            setSnackbarMessage(`${getErrorMessage(error)}`, 'error')
            return;
        }
        setLoading(false)
    }, [isError, navigate, setSnackbarMessage, isValidUnitMeasureId, error]);

    if (isLoading || loading) {
        return <CircularLoadingProgress />
    }

    return <UnitMeasureNewEdit unitMeasureData={data} />
}