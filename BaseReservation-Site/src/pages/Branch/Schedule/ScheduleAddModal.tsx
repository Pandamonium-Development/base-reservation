import { getDayInSpanish } from "utils/util";
import { Modal } from "components/Modal/Modal";
import { useSnackbar } from "stores/useSnackbar";
import { yupResolver } from "@hookform/resolvers/yup";
import { ModalBody } from "components/Modal/ModalBody";
import { ModalHeader } from "components/Modal/ModalHeader";
import { ErrorProcess } from "components/Error/ErrorProcess";
import { Controller, FormProvider, useForm } from "react-hook-form";
import { BranchSchedule, Schedule } from "types/api-basereservation";
import { FormButtonsModal } from "components/Shared/FormButtonsModal";
import { FormFieldErrorMessage } from "components/FormFieldErrorMessage";
import { ScheduleDefaultValues, ScheduleSchema } from "./ScheduleSchema";
import { UseGetSchedules } from "hooks/api-basereservation/schedule/UseGetSchedules";
import { CircularLoadingProgress } from "components/LoadingProgress/CircularLoadingProcess";
import { Alert, Box, FormControl, InputLabel, MenuItem, Select, Stack } from "@mui/material";

interface ScheduleAddModalProps {
    isModalOpen: boolean
    toggleIsOpen: () => void
    branchId: number
    schedulesAdded: BranchSchedule[]
    addSchedule: (schedule: BranchSchedule) => void
}

export const ScheduleAddModal = ({
    isModalOpen,
    toggleIsOpen,
    branchId,
    schedulesAdded,
    addSchedule
}: ScheduleAddModalProps) => {
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    const { data, isLoading, isError } = UseGetSchedules()

    const formMethods = useForm({
        resolver: yupResolver(ScheduleSchema),
        defaultValues: ScheduleDefaultValues
    });

    const {
        control,
        handleSubmit,
        formState: { errors },
        reset
    } = formMethods;

    const createScheduleWrapper = handleSubmit((dataSubmit) => {
        const branchSchedule: BranchSchedule = {
            branchId,
            ...dataSubmit,
            schedule: data?.find((s) => s.id == dataSubmit.scheduleId)
        }
        addSchedule(branchSchedule);
        reset({ scheduleId: 0 });
        setSnackbarMessage('Horario agregado')
    })

    const filteredSchedules = data?.filter((horario) =>
        !schedulesAdded.some((schedule) => schedule.schedule?.id === horario.id)
    );

    return (
        <Modal
            isOpen={isModalOpen}
            toggleIsOpen={toggleIsOpen}
            sx={{
                width: { xs: '90vw', sm: '40%' },
                height: 'auto'
            }}
        >
            <ModalHeader
                toggleIsOpen={toggleIsOpen}
                title="Agregar horario"
            />
            <ModalBody heightModal="10%">
                {isLoading && <CircularLoadingProgress />}
                {isError && <ErrorProcess />}

                {!isLoading && !isError && (
                    <FormProvider {...formMethods}>
                        <form onSubmit={createScheduleWrapper} noValidate>
                            <Box pb={2}>
                                {Object.keys(errors).length > 0 && (
                                    <Alert severity="error">Por favor corrija los errores para continuar</Alert>
                                )}
                            </Box>
                            <Stack spacing={4}>
                                <Box>
                                    <Controller
                                        name="scheduleId"
                                        control={control}
                                        render={({ field }) => (
                                            <Stack direction='column' gap={1}>
                                                <InputLabel sx={{ fontSize: '1rem' }} htmlFor='schedule' required>
                                                    Horario
                                                </InputLabel>
                                                <FormControl fullWidth error={!!errors.scheduleId}>
                                                    <Select
                                                        id='schedule'
                                                        value={field.value}
                                                        onChange={(e) => field.onChange(e.target.value)}
                                                    >
                                                        <MenuItem key={0} value={0}>
                                                            Seleccione el horario
                                                        </MenuItem>
                                                        {filteredSchedules?.map((horario: Schedule) => (
                                                            <MenuItem key={horario.id} value={horario.id}>
                                                                {`${getDayInSpanish(horario.day)}: ${horario.startHour} - ${horario.endHour}`}
                                                            </MenuItem>
                                                        ))}
                                                    </Select>
                                                </FormControl>
                                            </Stack>
                                        )}
                                    />
                                    {errors.scheduleId?.message && (
                                        <FormFieldErrorMessage message={errors.scheduleId.message} />
                                    )}
                                </Box>

                                <FormButtonsModal onCloseModal={toggleIsOpen} />
                            </Stack>
                        </form>
                    </FormProvider>
                )}
            </ModalBody>
        </Modal>
    )
}