import { isNil } from "lodash";
import { TaxNewEdit } from "./TaxNewEdit";
import { useEffect, useState } from "react";
import { getErrorMessage } from "utils/util";
import { useSnackbar } from "stores/useSnackbar";
import { useNavigate, useParams } from "react-router-dom";
import { UseGetTaxById } from "hooks/api-basereservation/tax/useGetTaxById";
import { CircularLoadingProgress } from "components/LoadingProgress/CircularLoadingProcess";


export const TaxNewEditWrapper = () => {
    const { taxId } = useParams<{ taxId?: string }>();
    const navigate = useNavigate();
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    const { data, isLoading, isError, error } = UseGetTaxById(taxId);

    const [loading, setLoading] = useState<boolean>(true);

    const isValidTaxId = isNil(taxId) || !isNil(taxId) && !isNaN(Number(taxId));

    useEffect(() => {
        if (!isValidTaxId) {
            navigate('/General/Impuesto');
            return;
        }
        if (isError) {
            navigate('/General/Impuesto');
            setSnackbarMessage(`${getErrorMessage(error)}`, 'error')
            return;
        }
        setLoading(false)
    }, [isError, navigate, setSnackbarMessage, isValidTaxId, error]);

    if (isLoading || loading) {
        return <CircularLoadingProgress />
    }

    return <TaxNewEdit taxData={data} />
}