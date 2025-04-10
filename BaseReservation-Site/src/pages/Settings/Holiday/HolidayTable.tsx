import { useNavigate } from "react-router-dom";
import { GridColDef, GridEventListener, GridRowParams } from "@mui/x-data-grid";
import { UseGetHolidays } from "hooks/api-basereservation/holiday/UseGetHolidays";
import DataTableWrapper from "components/Table/DataTableWrapper"; // Importar el componente genérico

export const HolidayTable = () => {
    const { data, isLoading, isError } = UseGetHolidays();
    const navigate = useNavigate();

    const columns: GridColDef[] = [
        { field: 'id', headerName: 'Id', minWidth: 20, flex: 1 },
        { field: 'name', headerName: 'Nombre', minWidth: 130, flex: 1 },
        { field: 'month', headerName: 'Mes', minWidth: 100, flex: 1 },
        { field: 'day', headerName: 'Día base', minWidth: 100, flex: 1 },
    ];

    const selectRow: GridEventListener<'rowClick'> = (params: GridRowParams) => {
        navigate(`/General/Feriado/${params.id}`);
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
