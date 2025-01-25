import { Container } from "@mui/material";
import { NavHeader } from "components/Menu/NavHeader";

export const Header = () => {
    return (
        <Container maxWidth={false} disableGutters>
            <NavHeader />
        </Container>
    )
};