import { Button } from "@mui/material"
import { Link } from "react-router-dom"
import { Page } from "components/Shared/Page"
import AddIcon from '@mui/icons-material/Add';
import { HolidayTable } from "./HolidayTable";
import { PageHeader } from "components/Shared/PageHeader"

export const Holiday = () => {
    return (
        <Page
            header={
                <PageHeader
                    title="Feriados"
                    actionButton={
                        <Link to='/General/Feriado/Nuevo'>
                            <Button variant="contained" size="large" fullWidth startIcon={<AddIcon />}>Crear Feriado</Button>
                        </Link>
                    }
                />
            }
        >
            <HolidayTable />
        </Page>
    )
}