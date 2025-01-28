import { useState } from "react"
import { useNavigate } from "react-router-dom"
import { Branch } from "types/api-basereservation"
import { DataTable } from "components/Table/DataTable"
import { ErrorProcess } from "components/Error/ErrorProcess"
import { OptionsBullet } from "components/Table/OptionsBullet"
import { CircularProgress, Menu, MenuItem } from "@mui/material"
import { useGetBranches } from "hooks/api-basereservation/useGetBranches"
import { GridColDef, GridEventListener, GridRenderCellParams, GridRowParams } from "@mui/x-data-grid"

export const BranchTable = () => {
    const navigate = useNavigate()
    const branchItemsQuery = useGetBranches()
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null)

    const handleMenuOpen = (event: React.MouseEvent<HTMLButtonElement>) => { setAnchorEl(event.currentTarget) }
    const handleMenuClose = () => { setAnchorEl(null) }

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
        },
        {
            field: 'email',
            headerName: 'Correo electrónico',
            minWidth: 300,
        },
        {
            field: '',
            align: 'right',
            headerName: 'Opciones',
            minWidth: 150,
            renderCell: (params: GridRenderCellParams<Branch>) => {
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
                                    navigate(`/Sucursal/${params.id}/Horario`)
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
        return <CircularProgress />
    }

    if (branchItemsQuery.isError) {
        return <ErrorProcess />
    }

    const selectRow: GridEventListener<'rowClick'> = (params: GridRowParams) => {
        navigate(`/Sucursal/${params.id}`)
    }

    return (
        <DataTable
            columns={columns}
            rows={branchItemsQuery.data}
            onRowClick={selectRow}
        />
    )
}