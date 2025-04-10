import { getDayInSpanish } from "utils/util";
import { useNavigate } from "react-router-dom";
import { Schedule } from "types/api-basereservation";
import DataTableWrapper from "components/Table/DataTableWrapper";
import { UseGetSchedules } from "hooks/api-basereservation/schedule/UseGetSchedules";
import { GridColDef, GridRenderCellParams, GridEventListener, GridRowParams } from "@mui/x-data-grid";

export const ScheduleTable = () => {
    const { data, isLoading, isError } = UseGetSchedules();
    const navigate = useNavigate();

    const columns: GridColDef[] = [
        { field: 'id', headerName: 'Id', minWidth: 20, flex: 1 },
        {
            field: 'day',
            headerName: 'Día',
            minWidth: 100,
            flex: 1,
            renderCell: (params: GridRenderCellParams<Schedule>) => getDayInSpanish(params.row.day),
        },
        { field: 'startHour', headerName: 'Hora inicial', minWidth: 100, flex: 1 },
        { field: 'endHour', headerName: 'Hora final', minWidth: 100, flex: 1 },
    ];

    const selectRow: GridEventListener<'rowClick'> = (params: GridRowParams) => {
        navigate(`/General/Horario/${params.id}`);
    };

    return (
        <DataTableWrapper
            columns={columns}
            data={data ?? []}
            loading={isLoading}
            error={isError}
            onRowClick={selectRow}
        />
    );
};
