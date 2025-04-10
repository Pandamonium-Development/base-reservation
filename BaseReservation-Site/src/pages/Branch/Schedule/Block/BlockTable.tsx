
import { useNavigate } from "react-router-dom";
import { BranchScheduleBlock } from "types/api-basereservation"
import DataTableWrapper from "components/Table/DataTableWrapper";
import { GridColDef, GridEventListener, GridRowParams } from "@mui/x-data-grid";

interface BlockTableProps {
    branchId: number,
    scheduleId: number,
    blocks: BranchScheduleBlock[]
}

export const BlockTable = (
    {
        branchId,
        scheduleId,
        blocks
    }: BlockTableProps) => {
    const navigate = useNavigate();

    const columns: GridColDef[] = [
        {
            field: 'startHour',
            headerName: 'Hora de inicio',
            minWidth: 200,
            flex: 1
        },
        {
            field: 'endHour',
            headerName: 'Hora de fin',
            minWidth: 200,
            flex: 1
        },
    ]

    const selectRow: GridEventListener<'rowClick'> = (params: GridRowParams) => {
        navigate(`/Sucursal/${branchId}/Horario/${scheduleId}/Bloqueo/${params.id}`)
    }

    return (
        <DataTableWrapper
            sort="asc"
            columns={columns}
            data={blocks}
            onRowClick={selectRow}
        />
    )
}