import { useNavigate } from 'react-router-dom';
import { useSnackbar } from 'stores/useSnackbar';
import { BaseReservationErrorDetails } from 'types/api-basereservation';

export const UseMutationCallbacks = (successMessage: string, redirectTo: string, onSettledCallback?: () => void) => {
    const navigate = useNavigate();
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    return {
        onSuccess: () => {
            setSnackbarMessage(successMessage);
            navigate(redirectTo);
        },
        onError: (data: BaseReservationErrorDetails) => {
            setSnackbarMessage(`${data.message}`, 'error');
        },
        onSettled: () => {
            onSettledCallback?.();
        },
    };
};