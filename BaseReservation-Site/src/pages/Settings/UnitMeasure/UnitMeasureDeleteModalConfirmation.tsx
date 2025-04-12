import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Modal } from "components/Modal/Modal";
import { useSnackbar } from "stores/useSnackbar"
import { ModalBody } from "components/Modal/ModalBody";
import { Button, Stack, Typography } from "@mui/material";
import { ModalFooter } from "components/Modal/ModalFooter";
import { ModalHeader } from "components/Modal/ModalHeader";
import { BaseReservationErrorDetails } from "types/api-basereservation";
import { UseDeleteUnitMeasure } from "hooks/api-basereservation/unitMeasure/UseDeleteUnitMeasureById";

interface UnitMeasureDeleteModalConfirmationProps {
    isModalOpen: boolean
    toggleIsOpen: () => void
    unitMeasureId: number
    title: string
}
export const UnitMeasureDeleteModalConfirmation = ({
    isModalOpen,
    toggleIsOpen,
    unitMeasureId,
    title
}: UnitMeasureDeleteModalConfirmationProps) => {
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    const { mutate: deleteUnitMeasure } = UseDeleteUnitMeasure({
        onSuccess: (data: boolean) => {
            setSnackbarMessage(data ? "Unidad de medida eliminada correctamente" : "Error al eliminar unidad de medida", data ? "success" : "error");
            toggleIsOpen();
            navigate('/General/UnidadMedida')
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
        deleteUnitMeasure(unitMeasureId)
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
                title="Eliminación de unidad de medida"
                subTitle={`${title}`}
            />
            <ModalBody heightModal="10%">
                <Typography fontSize={24} fontWeight={'bold'}>
                    ¿Esta seguro que desear eliminar la unidad de medida?
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