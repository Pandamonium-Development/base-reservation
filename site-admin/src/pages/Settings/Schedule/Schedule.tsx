import { Button } from "@mui/material"
import { Link } from "react-router-dom"
import { Page } from "components/Shared/Page"
import AddIcon from '@mui/icons-material/Add';
import { ScheduleTable } from "./ScheduleTable";
import { PageHeader } from "components/Shared/PageHeader"

export const Schedule = () => {
    return (
        <Page
            header={
                <PageHeader
                    title="Horarios"
                    actionButton={
                        <Link to='/General/Horario/Nuevo'>
                            <Button variant="contained" size="large" fullWidth startIcon={<AddIcon />}>Crear Horario</Button>
                        </Link>
                    }
                />
            }
        >
            <ScheduleTable />
        </Page>
    )
}