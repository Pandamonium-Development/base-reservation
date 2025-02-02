import { Menu, MenuItem } from "@mui/material";
import { GridColDef, GridEventListener, GridRenderCellParams, GridRowParams } from "@mui/x-data-grid";
import { ErrorProcess } from "components/Error/ErrorProcess";
import { CircularLoadingProgress } from "components/LoadingProgress/CircularLoadingProcess";
import { DataTable } from "components/Table/DataTable";
import { OptionsBullet } from "components/Table/OptionsBullet";
import { useGetBranchSchedules } from "hooks/api-basereservation/branch/schedule/useGetBranchSchedules";
import { isNil } from "lodash";
import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { useSnackbar } from "stores/useSnackbar";
import { BranchSchedule } from "types/api-basereservation";
import { getErrorMessage } from "utils/util";

export const ScheduleTable = ({ branchId }: { branchId: number }) => {
    const navigate = useNavigate();
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null)
    const handleMenuOpen = (event: React.MouseEvent<HTMLButtonElement>) => { setAnchorEl(event.currentTarget) }
    const handleMenuClose = () => { setAnchorEl(null) }

    const { data, isLoading, isError, error } = useGetBranchSchedules(branchId);

    const [loading, setLoading] = useState<boolean>(true);

    const isValidBranchId = isNil(branchId) || !isNil(branchId) && !isNaN(Number(branchId));

    useEffect(() => {
        if (!isValidBranchId) {
            navigate('/Sucursal');
            return;
        }
        if (isError) {
            navigate('/Sucursal');
            setSnackbarMessage(`${getErrorMessage(error)}`, 'error')
            return;
        }
        setLoading(false)
    }, [isError, navigate, setSnackbarMessage, isValidBranchId, error]);

    const columns: GridColDef[] = [
        {
            field: 'id',
            headerName: 'Id',
            minWidth: 20,
        },
        {
            field: 'schedule.day',
            headerName: 'Día',
            minWidth: 200,
        },
        {
            field: 'schedule.startHour',
            headerName: 'Inicio',
            minWidth: 250,
        },
        {
            field: 'schedule.endHour',
            headerName: 'Fin',
            minWidth: 250,
        },
        {
            field: 'opciones',
            align: 'right',
            headerName: 'Opciones',
            minWidth: 150,
            renderCell: (params: GridRenderCellParams<BranchSchedule>) => {
                return (
                    <>
                        <OptionsBullet handleMenuOpen={handleMenuOpen} />
                        <Menu
                            anchorEl={anchorEl}
                            open={Boolean(anchorEl)}
                            onClose={handleMenuClose}
                        >
                            <MenuItem
                                onClick={() => {
                                    handleMenuClose()
                                    navigate(`/Sucursal/${branchId}/Horario/${params.id}/Bloqueos`)
                                }}
                            >
                                Bloqueos
                            </MenuItem>

                        </Menu>
                    </>
                )
            }
        }
    ]

    const selectRow: GridEventListener<'rowClick'> = (params: GridRowParams) => {
        navigate(`/Sucursal/${params.id}`)
    }

    if (isLoading || loading) {
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