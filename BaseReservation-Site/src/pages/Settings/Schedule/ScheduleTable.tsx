import { GridColDef, GridEventListener, GridRenderCellParams, GridRowParams } from "@mui/x-data-grid"
import { DataTable } from "components/Table/DataTable"
import { ErrorProcess } from "components/Error/ErrorProcess"
import { useGetSchedules } from "hooks/api-basereservation/schedule/useGetSchedules"
import { CircularLoadingProgress } from "components/LoadingProgress/CircularLoadingProcess"
import { Schedule } from "types/api-basereservation"
import { getDayInSpanish } from "utils/util"
import { useNavigate } from "react-router-dom"

export const ScheduleTable = () => {
    const { data, isLoading, isError } = useGetSchedules()
    const navigate = useNavigate();

    const columns: GridColDef[] = [
        {
            field: 'id',
            headerName: 'Id',
            minWidth: 20,
            flex: 1
        },
        {
            field: 'day',
            headerName: 'Día',
            minWidth: 100,
            flex: 1,
            renderCell: (params: GridRenderCellParams<Schedule>) => {
                return getDayInSpanish(params.row.day)
            }
        },
        {
            field: 'startHour',
            headerName: 'Hora inicial',
            minWidth: 100,
            flex: 1
        },
        {
            field: 'endHour',
            headerName: 'Hora final',
            minWidth: 100,
            flex: 1
        },
    ]

    const selectRow: GridEventListener<'rowClick'> = (params: GridRowParams) => {
        navigate(`/General/Horario/${params.id}`)
    }

    if (isLoading) {
        return <CircularLoadingProgress />
    }

    if (isError) {
        return <ErrorProcess />
    }

    return (
        <DataTable
            sortFieldName="id"
            sort="desc"
            columns={columns}
            rows={data}
            onRowClick={selectRow}
        />
    )
}