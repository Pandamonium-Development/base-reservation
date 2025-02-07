import { useState } from "react"
import { getDayInSpanish } from "utils/util";
import { useNavigate } from "react-router-dom"
import { Menu, MenuItem } from "@mui/material";
import { DataTable } from "components/Table/DataTable";
import { BranchSchedule } from "types/api-basereservation";
import { OptionsBullet } from "components/Table/OptionsBullet";
import { GridColDef, GridRenderCellParams } from "@mui/x-data-grid";

export const ScheduleTable = ({ branchId, schedules }: { branchId: number, schedules: BranchSchedule[] }) => {
    const navigate = useNavigate();
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null)
    const [selectedRowId, setSelectedRowId] = useState<number | null>(null)

    const handleMenuOpen = (event: React.MouseEvent<HTMLButtonElement>, id: number) => {
        setAnchorEl(event.currentTarget)
        setSelectedRowId(id)
    }

    const handleMenuClose = () => {
        setAnchorEl(null);
        setSelectedRowId(null)
    }

    const columns: GridColDef[] = [
        {
            field: 'schedule.day',
            headerName: 'Día',
            minWidth: 200,
            renderCell: (params: GridRenderCellParams<BranchSchedule>) => {
                return getDayInSpanish(params.row.schedule?.day)
            },
            flex: 1
        },
        {
            field: 'schedule.startHour',
            headerName: 'Inicio',
            minWidth: 250,
            flex: 1
        },
        {
            field: 'schedule.endHour',
            headerName: 'Fin',
            minWidth: 250,
            flex: 1
        },
        {
            field: 'opciones',
            align: 'right',
            headerName: 'Opciones',
            minWidth: 150,
            renderCell: (params: GridRenderCellParams<BranchSchedule>) => {
                return (
                    <>
                        <OptionsBullet handleMenuOpen={(e) => handleMenuOpen(e, Number(params.row.id))} />
                        <Menu
                            anchorEl={anchorEl}
                            open={selectedRowId === params.row.id}
                            onClose={handleMenuClose}
                        >
                            <MenuItem
                                onClick={() => {
                                    handleMenuClose()
                                    navigate(`/Sucursal/${branchId}/Horario/${params.id}/Bloqueo`)
                                }}
                            >
                                Bloqueos
                            </MenuItem>

                        </Menu>
                    </>
                )
            },
            flex: 1
        }
    ]

    return (
        <DataTable
            sort="asc"
            columns={columns}
            rows={schedules}
        />
    )
}