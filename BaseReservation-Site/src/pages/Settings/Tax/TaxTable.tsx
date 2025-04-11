import { useNavigate } from "react-router-dom";
import DataTableWrapper from "components/Table/DataTableWrapper";
import { UseGetTaxes } from "hooks/api-basereservation/tax/UseGetTaxes";
import { GridColDef, GridEventListener, GridRenderCellParams, GridRowParams } from "@mui/x-data-grid";
import { Tax } from "types/api-basereservation";

export const TaxTable = () => {
    const { data, isLoading, isError } = UseGetTaxes();
    const navigate = useNavigate();

    const columns: GridColDef[] = [
        { field: 'id', headerName: 'Id', minWidth: 20, flex: 1 },
        { field: 'name', headerName: 'Nombre', minWidth: 130, flex: 1 },
        { field: 'rate', headerName: 'Tasa', align: 'right', minWidth: 100, flex: 1, renderCell: (params: GridRenderCellParams<Tax>) => `${params.row.rate} %`, },
    ];

    const selectRow: GridEventListener<'rowClick'> = (params: GridRowParams) => {
        navigate(`/General/Impuesto/${params.id}`);
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
