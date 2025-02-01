import { Button } from "@mui/material";
import { Link } from "react-router-dom";
import { BranchTable } from "./BranchTable";
import { Page } from "components/Shared/Page";
import AddIcon from '@mui/icons-material/Add';
import { PageHeader } from "components/Shared/PageHeader";

export const Branch = () => {
    return (
        <Page
            header={
                <PageHeader title="Sucursales" actionButton={
                    <Link to="/Sucursal/Nueva">
                        <Button variant="contained" size="large" fullWidth startIcon={<AddIcon />}>Crear Sucursal</Button>
                    </Link>
                } />
            }
        >
            <BranchTable />
        </Page>
    );
}