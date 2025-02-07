import { GridColDef } from "@mui/x-data-grid";
import { DataTable } from "components/Table/DataTable";
import { BranchScheduleBlock } from "types/api-basereservation"

interface BlockTableProps {
    blocks: BranchScheduleBlock[]
}

export const BlockTable = ({ blocks }: BlockTableProps) => {
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
        }
    ]

    return (
        <DataTable
            sort="asc"
            columns={columns}
            rows={blocks}
        />
    )
}