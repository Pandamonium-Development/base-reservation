import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Modal } from "components/Modal/Modal";
import { useSnackbar } from "stores/useSnackbar"
import { ModalBody } from "components/Modal/ModalBody";
import { Button, Stack, Typography } from "@mui/material";
import { ModalFooter } from "components/Modal/ModalFooter";
import { ModalHeader } from "components/Modal/ModalHeader";
import { BaseReservationErrorDetails } from "types/api-basereservation";
import { UseDeleteTax } from "hooks/api-basereservation/tax/UseDeleteTaxById";

interface TaxDeleteModalConfirmationProps {
    isModalOpen: boolean
    toggleIsOpen: () => void
    taxId: number
    title: string
}
export const TaxDeleteModalConfirmation = ({
    isModalOpen,
    toggleIsOpen,
    taxId,
    title
}: TaxDeleteModalConfirmationProps) => {
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    const { mutate: deleteTax } = UseDeleteTax({
        onSuccess: (data: boolean) => {
            setSnackbarMessage(data ? "Impuesto eliminado correctamente" : "Error al eliminar impuesto", data ? "success" : "error");
            toggleIsOpen();
            navigate('/General/Impuesto')
        },
        onError: (data: BaseReservationErrorDetails) => {
            setSnackbarMessage(`${data.message}`, 'error');
            toggleIsOpen();
        },
        onSettled: () => {
            toggleIsOpen();
            setLoading(false);
        }
    })

    const handleConfirm = () => {
        setLoading(true);
        deleteTax(taxId)
    }

    return (
        <Modal
            isOpen={isModalOpen}
            toggleIsOpen={toggleIsOpen}
            sx={{
                width: { xs: '90vw', sm: '50%' },
                height: 'auto'
            }}
        >
            <ModalHeader
                toggleIsOpen={toggleIsOpen}
                title="Eliminación de impuesto"
                subTitle={`${title}`}
            />
            <ModalBody heightModal="10%">
                <Typography fontSize={24} fontWeight={'bold'}>
                    ¿Esta seguro que desear eliminar el impuesto?
                </Typography>
            </ModalBody>
            <ModalFooter>
                <Stack direction='row' spacing={2}>
                    <Button variant="outlined" onClick={toggleIsOpen}>Cancelar</Button>
                    <Button
                        loading={loading}
                        loadingPosition="start"
                        variant="contained"
                        onClick={handleConfirm}
                    >
                        Confirmar
                    </Button>
                </Stack>
            </ModalFooter>

        </Modal >
    )
}