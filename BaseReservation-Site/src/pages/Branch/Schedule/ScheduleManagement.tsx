import { isNil } from "lodash";
import { useEffect, useState } from "react";
import { Box, Button } from "@mui/material";
import AddIcon from '@mui/icons-material/Add';
import { Page } from "components/Shared/Page";
import { useSnackbar } from "stores/useSnackbar";
import { ScheduleAddModal } from "./ScheduleAddModal";
import { useNavigate, useParams } from "react-router-dom";
import { PageHeader } from "components/Shared/PageHeader";
import { FormButtons } from "components/Shared/FormButtons";
import { getDayInSpanish, getErrorMessage } from "utils/util";
import { ListViewWithDelete } from "components/ListView/ListViewWithDelete";
import { UseGetBranchById } from "hooks/api-basereservation/branch/UseGetBranchById";
import { CircularLoadingProgress } from "components/LoadingProgress/CircularLoadingProcess";
import { UsePostBranchSchedules } from "hooks/api-basereservation/branch/schedule/UsePostBranchSchedules";
import { BaseReservationErrorDetails, BranchSchedule, BranchScheduleRequest } from "types/api-basereservation";

export const ScheduleManagement = () => {
    const { branchId } = useParams<{ branchId?: string }>();
    const navigate = useNavigate();
    const [loading, setLoading] = useState<boolean>(true);
    const [loadingSubmit, setLoadingSubmit] = useState<boolean>(false);
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);
    const [existingSchedules, setExistingSchedules] = useState<Array<BranchSchedule>>([]);
    const [isOpenModalSchedule, setIsOpenModalSchedule] = useState(false);

    const { data: branch, isLoading, isError, error } = UseGetBranchById(branchId);

    const isValidBranchId = isNil(branchId) || !isNil(branchId) && !isNaN(Number(branchId));

    const handleDelete = (ids: number[]) => {
        setExistingSchedules(prevSchedules => prevSchedules.filter(schedule => !ids.includes(Number(schedule.id))));
    };

    const handleAdd = (branchSchedule: BranchSchedule) => {
        setExistingSchedules(prevSchedules => [...prevSchedules, branchSchedule]);
        setIsOpenModalSchedule(false)
    }

    const { mutate: postBranchSchedules } = UsePostBranchSchedules({
        branchId: Number(branchId),
        onSuccess() {
            setSnackbarMessage('Horarios asignados a la sucursal');
            navigate(`/Sucursal/${Number(branchId)}/Horario`);
        },
        onError(data: BaseReservationErrorDetails) {
            setSnackbarMessage(`${data.message}`, 'error');
        },
        onSettled() {
            setLoadingSubmit(false);
        }
    })

    const createBranchSchedulesWrapper = (event: React.FormEvent) => {
        event.preventDefault();

        setLoadingSubmit(true);
        postBranchSchedules(existingSchedules.length > 0
            ? existingSchedules.map((schedule): BranchScheduleRequest => {
                return { branchId: schedule.branchId, scheduleId: schedule.scheduleId }
            })
            : [] as BranchScheduleRequest[]
        );
    };

    useEffect(() => {
        if (!isValidBranchId) {
            navigate(`/Sucursal/${branchId}/Horario`);
            return;
        }
        if (isError) {
            navigate(`/Sucursal/${branchId}/Horario`);
            setSnackbarMessage(`${getErrorMessage(error)}`, 'error')
            return;
        }
        setExistingSchedules(branch?.branchSchedules ?? []);
        setLoading(false)
    }, [isError, navigate, setSnackbarMessage, isValidBranchId, error, branchId, branch?.branchSchedules]);

    if (isLoading || loading) {
        return <CircularLoadingProgress />
    }

    return (
        <Page
            header={
                <PageHeader
                    title="Gestión de horarios"
                    subtitle={branch?.name ?? ''}
                    backText="horarios"
                    backPath={`/Sucursal/${Number(branchId)}/Horario`}
                    actionButton={
                        <Button variant="contained" onClick={() => setIsOpenModalSchedule(true)} size="large" fullWidth startIcon={<AddIcon />}>Agregar horario</Button>
                    }
                />
            }
        >
            <Box sx={{ maxWidth: '600px' }}>
                <form onSubmit={createBranchSchedulesWrapper} noValidate>
                    <ListViewWithDelete<"ResponseBranchScheduleDto">
                        title="Horarios disponibles"
                        data={existingSchedules}
                        enableDense={true}
                        fieldForPrimaryText={(item: BranchSchedule) => String(`${getDayInSpanish(item.schedule?.day)}, de ${item.schedule?.startHour} a ${item.schedule?.endHour}`)}
                        onDelete={handleDelete} />

                    <FormButtons backPath={`/Sucursal/${Number(branchId)}/Horario`} loadingIndicator={loadingSubmit} />
                </form>

                <ScheduleAddModal
                    isModalOpen={isOpenModalSchedule}
                    branchId={Number(branchId)}
                    toggleIsOpen={() => setIsOpenModalSchedule(!isOpenModalSchedule)}
                    schedulesAdded={existingSchedules}
                    addSchedule={handleAdd}
                />
            </Box>

        </Page>
    )
}