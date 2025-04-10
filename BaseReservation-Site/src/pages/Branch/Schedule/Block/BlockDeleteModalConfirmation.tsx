import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Modal } from "components/Modal/Modal";
import { useSnackbar } from "stores/useSnackbar"
import { ModalBody } from "components/Modal/ModalBody";
import { Button, Stack, Typography } from "@mui/material";
import { ModalFooter } from "components/Modal/ModalFooter";
import { ModalHeader } from "components/Modal/ModalHeader";
import { BaseReservationErrorDetails } from "types/api-basereservation";
import { UseDeleteScheduleBlock } from "hooks/api-basereservation/branch/schedule/block/UseDeleteScheduleBlockById";

interface BlockDeleteModalConfirmationProps {
    isModalOpen: boolean
    toggleIsOpen: () => void
    branchId: number
    scheduleId: number
    blockId: number
    title: string
}
export const BlockDeleteModalConfirmation = ({
    isModalOpen,
    toggleIsOpen,
    branchId,
    scheduleId,
    blockId,
    title
}: BlockDeleteModalConfirmationProps) => {
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    const { mutate: deleteBranchScheduleBlock } = UseDeleteScheduleBlock({
        onSuccess: (data: boolean) => {
            setSnackbarMessage(data ? "Bloqueo eliminado correctamente" : "Error al eliminar bloqueo", data ? "success" : "error");
            toggleIsOpen();
            navigate(`/Sucursal/${branchId}/Horario/${scheduleId}/Bloqueo`)
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
        deleteBranchScheduleBlock(blockId)
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
                title="Eliminación de bloqueo"
                subTitle={`Nombre: ${title}`}
            />
            <ModalBody heightModal="10%">
                <Typography fontSize={24} fontWeight={'bold'}>
                    ¿Esta seguro que desear eliminar el bloqueo?
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