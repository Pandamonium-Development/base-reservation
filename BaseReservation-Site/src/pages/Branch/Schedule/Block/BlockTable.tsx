import { GridColDef, GridEventListener, GridRowParams } from "@mui/x-data-grid";
import { DataTable } from "components/Table/DataTable";
import { useNavigate } from "react-router-dom";
import { BranchScheduleBlock } from "types/api-basereservation"

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
            field: 'id',
            headerName: 'Id',
            minWidth: 20,
            flex: 1
        },
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
        <DataTable
            sort="asc"
            columns={columns}
            rows={blocks}
            onRowClick={selectRow}
        />
    )
}