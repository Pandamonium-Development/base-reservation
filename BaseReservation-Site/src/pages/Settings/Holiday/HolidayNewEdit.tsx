import { isNil } from "lodash";
import { useState } from "react";
import { useLayout } from "hooks/useLayout";
import { useNavigate } from "react-router-dom";
import { useSnackbar } from "stores/useSnackbar";
import { HolidayRequest, MonthName } from "types/api-basereservation";
import { HolidayDefaultValues, HolidaySchema } from "./HolidaySchema";
import { yupResolver } from "@hookform/resolvers/yup";
import { Controller, FormProvider, useForm } from "react-hook-form";
import { usePostHoliday } from "hooks/api-basereservation/holiday/usePostHoliday";
import { usePutHoliday } from "hooks/api-basereservation/holiday/usePutHoliday";
import { Page } from "components/Shared/Page";
import { PageHeader } from "components/Shared/PageHeader";
import { Alert, Box, Button, Stack, TextField } from "@mui/material";
import { isPresent } from "utils/util";
import { FormFieldErrorMessage } from "components/FormFieldErrorMessage";
import { MonthSelect } from "components/Misc/MonthSelect";
import { FormButtons } from "components/Shared/FormButtons";
import { DayOfMonthPicker } from "components/Misc/DayOfMonthPicker";
import { HolidayDeleteModalConfirmation } from "./HolidayDeleteModalConfirmation";

export const HolidayNewEdit = ({ holidayData }: { holidayData: HolidayRequest | undefined }) => {
    const navigate = useNavigate();
    const { isMobile } = useLayout();
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    const [loading, setLoading] = useState(false);
    const [month, setMonth] = useState<MonthName>(holidayData?.month ?? 'Enero');
    const formTitle = isNil(holidayData) ? 'Crear nuevo feriado' : `Editar feriado número ${holidayData.id}`;
    const isExisting = !isNil(holidayData);
    const fullHolidayDescription = `${holidayData?.name}, ${holidayData?.day ?? ''} de ${holidayData?.month ?? ''}`

    const [openModalConfirmation, setOpenModalConfirmation] = useState(false);

    const formMethods = useForm({
        resolver: yupResolver(HolidaySchema),
        defaultValues: isNil(holidayData) ? {
            id: HolidayDefaultValues.id,
            name: HolidayDefaultValues.name,
            month: HolidayDefaultValues.month as MonthName,
            day: HolidayDefaultValues.day
        } : {
            id: Number(holidayData.id),
            name: String(holidayData.name),
            month: holidayData.month,
            day: holidayData.day,
        }
    });

    const {
        control,
        register,
        handleSubmit,
        formState: { errors },
    } = formMethods;

    const { mutate: postHoliday } = usePostHoliday({
        onSuccess() {
            setSnackbarMessage('Feriado creado correctamente');
            navigate('/General/Feriado');
        },
        onError(data) {
            setSnackbarMessage(`${data.message}`, 'error');
        },
        onSettled() {
            setLoading(false);
        }
    })

    const { mutate: putHoliday } = usePutHoliday({
        onSuccess() {
            setSnackbarMessage('Feriado actualizado correctamente');
            navigate('/General/Feriado');
        },
        onError(data) {
            setSnackbarMessage(`${data.message}`, 'error');
        },
        onSettled() {
            setLoading(false);
        }
    })

    const createHolidayWrapper = handleSubmit((data) => {
        const formattedData = {
            name: data.name,
            month: data.month,
            day: data.day,
        }
        if (!isExisting) {
            postHoliday({ ...formattedData });
            return;
        }

        putHoliday({
            id: data.id,
            ...formattedData,
        })
    });

    return (
        <Page
            header={
                <PageHeader
                    title={formTitle}
                    subtitle="Debe completar los campos requeridos antes de guardar la información"
                    backText="Feriados"
                    backPath="/General/Feriado"
                    actionButton={
                        <Button sx={{ display: `${isExisting ? 'block' : 'none'}` }} variant="contained" size="large" fullWidth onClick={() => setOpenModalConfirmation(true)}>
                            Eliminar
                        </Button>
                    }
                />
            }
        >
            <FormProvider {...formMethods}>
                <form onSubmit={createHolidayWrapper} noValidate>
                    <Box pb={2}>
                        {Object.keys(errors).length > 0 && (
                            <Alert severity="error">Por favor corrija los errores para continuar</Alert>
                        )}
                    </Box>
                    <Stack spacing={4} maxWidth={isMobile ? '90vw' : '600px'}>
                        <Box>
                            <TextField
                                required
                                error={isPresent(errors.name)}
                                label="Nombre"
                                placeholder="Nombre del feriado"
                                fullWidth
                                {...register('name')}
                            />
                            {errors.name?.message && (
                                <FormFieldErrorMessage message={errors.name.message} />
                            )}
                        </Box>

                        <Box>
                            <Controller
                                name="month"
                                control={control}
                                defaultValue={month}
                                render={({ field }) => (
                                    <MonthSelect
                                        selectedMonth={month}
                                        onMonthChange={(newMonth: MonthName) => {
                                            field.onChange(newMonth);
                                            setMonth(newMonth);
                                        }}
                                        errorForm={!!errors.day}
                                    />
                                )}
                            />
                            {errors.month?.message && (
                                <FormFieldErrorMessage message={errors.month.message} />
                            )}
                        </Box>

                        <Box>
                            <Controller
                                name="day"
                                control={control}
                                render={({ field }) => (
                                    <DayOfMonthPicker
                                        field={field}
                                        selectedMonth={month}
                                        error={errors.day?.message}
                                    />
                                )}
                            />
                            {errors.day?.message && (
                                <FormFieldErrorMessage message={errors.day.message} />
                            )}
                        </Box>

                        <FormButtons backPath="/General/Feriado" loadingIndicator={loading} />
                    </Stack>
                </form>
            </FormProvider>

            <HolidayDeleteModalConfirmation
                isModalOpen={openModalConfirmation}
                scheduleId={holidayData?.id ?? 0}
                toggleIsOpen={() => setOpenModalConfirmation(!openModalConfirmation)}
                title={fullHolidayDescription}
            />

        </Page>
    )
}