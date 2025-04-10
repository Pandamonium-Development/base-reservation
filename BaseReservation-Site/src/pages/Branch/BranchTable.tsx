import { useState } from "react"
import { applyPhoneMask } from "utils/util"
import { Menu, MenuItem } from "@mui/material"
import { useNavigate } from "react-router-dom"
import { Branch } from "types/api-basereservation"
import { DataTable } from "components/Table/DataTable"
import { ErrorProcess } from "components/Error/ErrorProcess"
import { OptionsBullet } from "components/Table/OptionsBullet"
import { UseGetBranches } from "hooks/api-basereservation/branch/UseGetBranches"
import { CircularLoadingProgress } from "components/LoadingProgress/CircularLoadingProcess"
import { GridColDef, GridEventListener, GridRenderCellParams, GridRowParams } from "@mui/x-data-grid"

export const BranchTable = () => {
    const navigate = useNavigate()
    const branchItemsQuery = UseGetBranches()
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
            field: 'id',
            headerName: 'Id',
            minWidth: 20,
        },
        {
            field: 'name',
            headerName: 'Nombre',
            minWidth: 200,
        },
        {
            field: 'description',
            headerName: 'Descripción',
            minWidth: 250,
            flex: 1
        },
        {
            field: 'telephone',
            headerName: 'Teléfono',
            minWidth: 170,
            renderCell: (params: GridRenderCellParams<Branch>) => {
                return (
                    <>{applyPhoneMask(String(params.row.telephone))}</>
                )
            }
        },
        {
            field: 'email',
            headerName: 'Correo electrónico',
            minWidth: 300,
        },
        {
            field: 'opciones',
            align: 'right',
            headerName: 'Opciones',
            minWidth: 150,
            renderCell: (params: GridRenderCellParams<Branch>) => {
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
                                    navigate(`/Sucursal/${params.row.id}/Horario`)
                                }}
                            >
                                Horarios
                            </MenuItem>

                        </Menu>
                    </>
                )
            }
        }
    ]

    if (branchItemsQuery.isPending) {
        return <CircularLoadingProgress />
    }

    if (branchItemsQuery.isError) {
        return <ErrorProcess />
    }

    const selectRow: GridEventListener<'rowClick'> = (params: GridRowParams) => {
        navigate(`/Sucursal/${params.id}`)
    }

    return (
        <DataTable
            sortFieldName="id"
            sort="desc"
            columns={columns}
            rows={branchItemsQuery.data}
            onRowClick={selectRow}
        />
    )
}