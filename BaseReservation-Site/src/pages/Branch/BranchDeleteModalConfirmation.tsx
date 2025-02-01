import { useState } from "react";
import { Modal } from "components/Modal/Modal";
import { useSnackbar } from "stores/useSnackbar"
import { ModalBody } from "components/Modal/ModalBody";
import { Button, Stack, Typography } from "@mui/material";
import { ModalFooter } from "components/Modal/ModalFooter";
import { ModalHeader } from "components/Modal/ModalHeader";
import { BaseReservationErrorDetails } from "types/api-basereservation";
import { useDeleteBranch } from "hooks/api-basereservation/branch/useDeleteBranchById";
import { useNavigate } from "react-router-dom";

interface BranchDeleteModalConfirmationProps {
    isModalOpen: boolean
    toggleIsOpen: () => void
    branchId: number
    title: string
}
export const BranchDeleteModalConfirmation = ({
    isModalOpen,
    toggleIsOpen,
    branchId,
    title
}: BranchDeleteModalConfirmationProps) => {
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    const { mutate: deleteBranch } = useDeleteBranch({
        onSuccess: (data: boolean) => {
            setSnackbarMessage(data ? "Sucursal eliminada correctamente" : "Error al eliminar sucursal", data ? "success" : "error");
            toggleIsOpen();
            navigate('/Sucursal')
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
        deleteBranch(branchId)
    }

    return (
        <Modal
            isOpen={isModalOpen}
            toggleIsOpen={toggleIsOpen}
            sx={{
                width: { xs: '90vw', sm: '60%' },
                height: 'auto'
            }}
        >
            <ModalHeader
                toggleIsOpen={toggleIsOpen}
                title="Eliminación de sucursal"
                subTitle={`Nombre: ${title}`}
            />
            <ModalBody heightModal="10%">
                <Typography fontSize={24} fontWeight={'bold'}>
                    ¿Esta seguro que desear eliminar la sucursal?
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