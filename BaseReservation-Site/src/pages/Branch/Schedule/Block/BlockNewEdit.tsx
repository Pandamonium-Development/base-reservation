
import dayjs from "dayjs"
import { isNil } from "lodash"
import { useState } from "react"
import { useLayout } from "hooks/useLayout"
import { Page } from "components/Shared/Page"
import { useSnackbar } from "stores/useSnackbar"
import { yupResolver } from "@hookform/resolvers/yup"
import { PageHeader } from "components/Shared/PageHeader"
import { useNavigate, useParams } from "react-router-dom"
import { Alert, Box, Button, Stack } from "@mui/material"
import { FormButtons } from "components/Shared/FormButtons"
import { BranchScheduleBlock } from "types/api-basereservation"
import { BlockDefaultValues, BlockSchema } from "./BlockSchema"
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs'
import { Controller, FormProvider, useForm } from "react-hook-form"
import { LocalizationProvider, TimePicker } from "@mui/x-date-pickers"
import { FormFieldErrorMessage } from "components/FormFieldErrorMessage"
import { usePostScheduleBlock } from "hooks/api-basereservation/branch/schedule/block/usePostScheduleBlock"
import { usePutScheduleBlock } from "hooks/api-basereservation/branch/schedule/block/usePutScheduleBlock"
import { BlockDeleteModalConfirmation } from "./BlockDeleteModalConfirmation"
import { getDayInSpanish } from "utils/util"

export const BlockNewEdit = ({ branchScheduleBlockData }: { branchScheduleBlockData: BranchScheduleBlock | undefined | null }) => {
    const { branchId, scheduleId, blockId } = useParams<{ branchId?: string, scheduleId?: string, blockId?: string }>();

    const navigate = useNavigate();
    const { isMobile } = useLayout();
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);
    const [openModalConfirmation, setOpenModalConfirmation] = useState(false);

    const [loading, setLoading] = useState(false);

    const isValidBranchId = isNil(branchId) || !isNil(branchId) && !isNaN(Number(branchId));
    const isValidScheduleId = isNil(scheduleId) || !isNil(scheduleId) && !isNaN(Number(scheduleId));
    const isValidBlockId = isNil(blockId) || !isNil(blockId) && !isNaN(Number(blockId));

    const formTitle = isNil(branchScheduleBlockData) ? 'Crear nuevo bloqueo' : `Editar bloqueo número ${branchScheduleBlockData.id}`;
    const isExisting = !isNil(branchScheduleBlockData);

    const formMethods = useForm({
        resolver: yupResolver(BlockSchema),
        defaultValues: isNil(branchScheduleBlockData) ? BlockDefaultValues : {
            id: Number(branchScheduleBlockData.id),
            branchScheduleId: Number(branchScheduleBlockData.branchScheduleId),
            startHour: branchScheduleBlockData.startHour,
            endHour: branchScheduleBlockData.endHour
        }
    });

    const {
        control,
        handleSubmit,
        formState: { errors },
    } = formMethods;

    const { mutate: postBranchScheduleBlock } = usePostScheduleBlock({
        onSuccess() {
            setSnackbarMessage('Bloqueo creado correctamente');
            navigate(`/Sucursal/${branchId}/Horario/${scheduleId}/Bloqueo`);
        },
        onError(data) {
            setSnackbarMessage(`${data.message}`, 'error');
        },
        onSettled() {
            setLoading(false);
        }
    })

    const { mutate: putBranchScheduleBlock } = usePutScheduleBlock({
        onSuccess() {
            setSnackbarMessage('Bloqueo actualizado correctamente');
            navigate(`/Sucursal/${branchId}/Horario/${scheduleId}/Bloqueo`);
        },
        onError(data) {
            setSnackbarMessage(`${data.message}`, 'error');
        },
        onSettled() {
            setLoading(false);
        }
    })

    const createBlockWrapper = handleSubmit((data) => {
        setLoading(true);
        const formatedData = {
            branchScheduleId: Number(scheduleId),
            startHour: data.startHour,
            endHour: data.endHour,
            active: true,
        }
        if (!isExisting) {
            postBranchScheduleBlock({ ...formatedData });
            return;
        }

        putBranchScheduleBlock({
            id: data.id,
            ...formatedData,
        })
    })

    if (!isValidBranchId || !isValidScheduleId || (!isNil(blockId) && !isValidBlockId)) {
        setSnackbarMessage("Horario o sucursal no son válidos con el bloque", "error")
        navigate(`/Sucursal/${branchId}/Horario/${scheduleId}/Bloqueo`);
        return;
    }

    return (
        <Page
            header={
                <PageHeader
                    title={formTitle}
                    subtitle="Debe completar los campos requeridos antes de guardar la información"
                    backText="Bloqueos"
                    backPath={`/Sucursal/${branchId}/Horario/${scheduleId}/Bloqueo`}
                    actionButton={
                        <Button sx={{ display: `${isExisting ? 'block' : 'none'}` }} variant="contained" size="large" fullWidth onClick={() => setOpenModalConfirmation(true)}>
                            Eliminar
                        </Button>
                    }
                />
            }
        >
            <FormProvider {...formMethods}>
                <form onSubmit={createBlockWrapper} noValidate>
                    <Box pb={2}>
                        {Object.keys(errors).length > 0 && (
                            <Alert severity="error">Por favor corrija los errores para continuar</Alert>
                        )}
                    </Box>
                    <Stack spacing={4} maxWidth={isMobile ? '90vw' : '600px'}>
                        <LocalizationProvider dateAdapter={AdapterDayjs}>
                            <Box
                                sx={{
                                    display: 'flex',
                                    flexDirection: { xs: 'column', sm: 'row' },
                                    gap: 2,
                                }}
                            >
                                <Box sx={{ flex: 1 }}>
                                    <Controller
                                        name="startHour"
                                        control={control}
                                        render={({ field }) => (
                                            <TimePicker
                                                label="Hora de inicio"
                                                sx={{ width: '100%' }}
                                                value={field.value ? dayjs(field.value, 'HH:mm') : null}
                                                onChange={(newValue) => field.onChange(newValue?.format('HH:mm'))}
                                                format="HH:mm"
                                            />
                                        )}
                                    />
                                    {errors.startHour?.message && (
                                        <FormFieldErrorMessage message={errors.startHour.message} />
                                    )}
                                </Box>
                                <Box sx={{ flex: 1 }}>
                                    <Controller
                                        name="endHour"
                                        control={control}
                                        render={({ field }) => (
                                            <TimePicker
                                                label="Hora de fin"
                                                sx={{ width: '100%' }}
                                                value={field.value ? dayjs(field.value, 'HH:mm') : null}
                                                onChange={(newValue) => field.onChange(newValue?.format('HH:mm'))}
                                                format="HH:mm"
                                            />
                                        )}
                                    />
                                    {errors.endHour?.message && (
                                        <FormFieldErrorMessage message={errors.endHour.message} />
                                    )}
                                </Box>
                            </Box>
                        </LocalizationProvider>
                        <FormButtons backPath={`/Sucursal/${branchId}/Horario/${scheduleId}/Bloqueo`} loadingIndicator={loading} />
                    </Stack>
                </form>
            </FormProvider>

            <BlockDeleteModalConfirmation
                isModalOpen={openModalConfirmation}
                toggleIsOpen={() => setOpenModalConfirmation(!openModalConfirmation)}
                branchId={branchScheduleBlockData?.branchSchedule?.branchId ?? 0}
                scheduleId={branchScheduleBlockData?.branchScheduleId ?? 0}
                blockId={branchScheduleBlockData?.id ?? 0}
                title={`${branchScheduleBlockData?.branchSchedule?.branch?.name} - ${getDayInSpanish(branchScheduleBlockData?.branchSchedule?.schedule?.day)}`}
            />
        </Page >
    )
}