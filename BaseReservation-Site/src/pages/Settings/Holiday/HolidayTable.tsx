import { useNavigate } from "react-router-dom";
import { DataTable } from "components/Table/DataTable";
import { ErrorProcess } from "components/Error/ErrorProcess";
import { GridColDef, GridEventListener, GridRowParams } from "@mui/x-data-grid";
import { useGetHolidays } from "hooks/api-basereservation/holiday/useGetHolidays"; 
import { CircularLoadingProgress } from "components/LoadingProgress/CircularLoadingProcess";

export const HolidayTable = () => {
    const { data, isLoading, isError } = useGetHolidays()
    const navigate = useNavigate();

    const columns: GridColDef[] = [
        {
            field: 'id',
            headerName: 'Id',
            minWidth: 20,
            flex: 1
        },
        {
            field: 'name',
            headerName: 'Nombre',
            minWidth: 130,
            flex: 1,
        },
        {
            field: 'month',
            headerName: 'Mes',
            minWidth: 100,
            flex: 1
        },
        {
            field: 'day',
            headerName: 'Día base',
            minWidth: 100,
            flex: 1
        },
    ]

    const selectRow: GridEventListener<'rowClick'> = (params: GridRowParams) => {
        navigate(`/General/Feriado/${params.id}`)
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