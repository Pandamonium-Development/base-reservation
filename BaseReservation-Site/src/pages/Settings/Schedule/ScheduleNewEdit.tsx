import { isNil } from "lodash";
import { useState } from "react";
import { useLayout } from "hooks/useLayout";
import { getDayInSpanish } from 'utils/util';
import { Page } from "components/Shared/Page";
import { useNavigate } from "react-router-dom";
import { useSnackbar } from "stores/useSnackbar";
import { DaySelect } from "components/Misc/DaySelect";
import { yupResolver } from "@hookform/resolvers/yup";
import { PageHeader } from "components/Shared/PageHeader";
import { Alert, Box, Button, Stack } from "@mui/material";
import { FormButtons } from "components/Shared/FormButtons";
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { Controller, FormProvider, useForm } from "react-hook-form";
import { ScheduleRequest, WeeklyDay } from "types/api-basereservation";
import { ScheduleDefaultValues, ScheduleSchema } from "./ScheduleSchema";
import { FormFieldErrorMessage } from "components/FormFieldErrorMessage";
import { TimePickerField } from 'components/DateTimePickers/TimePickerField';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { usePutSchedule } from "hooks/api-basereservation/schedule/usePutSchedule";
import { ScheduleDeleteModalConfirmation } from './ScheduleDeleteModalConfirmation';
import { usePostSchedule } from "hooks/api-basereservation/schedule/usePostSchedule";

export const ScheduleNewEdit = ({ scheduleData }: { scheduleData: ScheduleRequest | undefined }) => {
    const navigate = useNavigate();
    const { isMobile } = useLayout();
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    const [loading, setLoading] = useState(false);
    const [day, setDay] = useState<WeeklyDay>(scheduleData?.day ?? 'Lunes');
    const formTitle = isNil(scheduleData) ? 'Crear nuevo horario' : `Editar sucursal número ${scheduleData.id}`;
    const isExisting = !isNil(scheduleData);
    const fullScheduleDescription = `${getDayInSpanish(scheduleData?.day)}, ${scheduleData?.startHour ?? ''} - ${scheduleData?.endHour ?? ''}`

    const [openModalConfirmation, setOpenModalConfirmation] = useState(false);

    const formMethods = useForm({
        resolver: yupResolver(ScheduleSchema),
        defaultValues: isNil(scheduleData) ? {
            id: ScheduleDefaultValues.id,
            day: ScheduleDefaultValues.day as WeeklyDay,
            startHour: ScheduleDefaultValues.startHour,
            endHour: ScheduleDefaultValues.endHour
        } : {
            id: Number(scheduleData.id),
            day: scheduleData.day,
            startHour: scheduleData.startHour,
            endHour: scheduleData.endHour
        }
    });

    const {
        control,
        handleSubmit,
        formState: { errors },
    } = formMethods;

    const { mutate: postSchedule } = usePostSchedule({
        onSuccess() {
            setSnackbarMessage('Horario creado correctamente');
            navigate('/General/Horario');
        },
        onError(data) {
            setSnackbarMessage(`${data.message}`, 'error');
        },
        onSettled() {
            setLoading(false);
        }
    })

    const { mutate: putSchedule } = usePutSchedule({
        onSuccess() {
            setSnackbarMessage('Horario actualizado correctamente');
            navigate('/General/Horario');
        },
        onError(data) {
            setSnackbarMessage(`${data.message}`, 'error');
        },
        onSettled() {
            setLoading(false);
        }
    })

    const createScheduleWrapper = handleSubmit((data) => {
        const formatedData = {
            day: data.day,
            startHour: data.startHour,
            endHour: data.endHour,
        }
        if (!isExisting) {
            postSchedule({ ...formatedData });
            return;
        }

        putSchedule({
            id: data.id,
            ...formatedData,
        })
    });

    return (
        <Page
            header={
                <PageHeader
                    title={formTitle}
                    subtitle="Debe completar los campos requeridos antes de guardar la información"
                    backText="Horarios"
                    backPath="/General/Horario"
                    actionButton={
                        <Button sx={{ display: `${isExisting ? 'block' : 'none'}` }} variant="contained" size="large" fullWidth onClick={() => setOpenModalConfirmation(true)}>
                            Eliminar
                        </Button>
                    }
                />
            }
        >
            <FormProvider {...formMethods}>
                <form onSubmit={createScheduleWrapper} noValidate>
                    <Box pb={2}>
                        {Object.keys(errors).length > 0 && (
                            <Alert severity="error">Por favor corrija los errores para continuar</Alert>
                        )}
                    </Box>
                    <Stack spacing={4} maxWidth={isMobile ? '90vw' : '600px'}>
                        <Box>
                            <Controller
                                name="day"
                                control={control}
                                defaultValue={day}
                                render={({ field }) => (
                                    <DaySelect
                                        selectedDay={day}
                                        onDayChange={(newDay: WeeklyDay) => {
                                            field.onChange(newDay);
                                            setDay(newDay);
                                        }}
                                        errorForm={!!errors.day}
                                    />
                                )}
                            />
                            {errors.day?.message && (
                                <FormFieldErrorMessage message={errors.day.message} />
                            )}
                        </Box>

                        <LocalizationProvider dateAdapter={AdapterDayjs}>
                            <Box
                                sx={{
                                    display: 'flex',
                                    flexDirection: { xs: 'column', sm: 'row' },
                                    gap: 2,
                                }}
                            >
                                <TimePickerField
                                    name="startHour"
                                    label="Hora de inicio"
                                    control={control}
                                    errors={errors}
                                />
                                <TimePickerField
                                    name="endHour"
                                    label="Hora de fin"
                                    control={control}
                                    errors={errors}
                                />
                            </Box>
                        </LocalizationProvider>
                        <FormButtons backPath="/General/Horario" loadingIndicator={loading} />
                    </Stack>
                </form>
            </FormProvider>

            <ScheduleDeleteModalConfirmation
                isModalOpen={openModalConfirmation}
                scheduleId={scheduleData?.id ?? 0}
                toggleIsOpen={() => setOpenModalConfirmation(!openModalConfirmation)}
                title={fullScheduleDescription}
            />
        </Page>
    );
}