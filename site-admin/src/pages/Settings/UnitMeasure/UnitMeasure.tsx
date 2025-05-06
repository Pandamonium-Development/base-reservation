import { Button } from "@mui/material"
import { Link } from "react-router-dom"
import { Page } from "components/Shared/Page"
import AddIcon from '@mui/icons-material/Add';
import { UnitMeasureTable } from "./UnitMeasureTable";
import { PageHeader } from "components/Shared/PageHeader"

export const UnitMeasure = () => {
    return (
        <Page
            header={
                <PageHeader
                    title="Unidades de medida"
                    actionButton={
                        <Link to='/General/UnidadMedida/Nuevo'>
                            <Button variant="contained" size="large" fullWidth startIcon={<AddIcon />}>Crear Unidad de medida</Button>
                        </Link>
                    }
                />
            }
        >
            <UnitMeasureTable />
        </Page>
    )
}