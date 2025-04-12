import { useNavigate } from "react-router-dom";
import DataTableWrapper from "components/Table/DataTableWrapper";
import { GridColDef, GridEventListener, GridRowParams } from "@mui/x-data-grid";
import { UseGetUnitMeasures } from "hooks/api-basereservation/unitMeasure/UseGetUnitMeasures";

export const UnitMeasureTable = () => {
    const { data, isLoading, isError } = UseGetUnitMeasures();
    const navigate = useNavigate();

    const columns: GridColDef[] = [
        { field: 'id', headerName: 'Id', minWidth: 20, flex: 1 },
        { field: 'name', headerName: 'Nombre', minWidth: 130, flex: 1 },
        { field: 'symbol', headerName: 'Símbolo', minWidth: 100, flex: 1 },
    ];

    const selectRow: GridEventListener<'rowClick'> = (params: GridRowParams) => {
        navigate(`/General/UnidadMedida/${params.id}`);
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
