import { Button } from "@mui/material"
import { Link } from "react-router-dom"
import { Page } from "components/Shared/Page"
import AddIcon from '@mui/icons-material/Add';
import { TaxTable } from "./TaxTable";
import { PageHeader } from "components/Shared/PageHeader"

export const Tax = () => {
    return (
        <Page
            header={
                <PageHeader
                    title="Impuestos"
                    actionButton={
                        <Link to='/General/Impuesto/Nuevo'>
                            <Button variant="contained" size="large" fullWidth startIcon={<AddIcon />}>Crear Impuesto</Button>
                        </Link>
                    }
                />
            }
        >
            <TaxTable />
        </Page>
    )
}