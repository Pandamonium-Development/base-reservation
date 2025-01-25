import { Drawer } from "@mui/material";
import { MenuOptions } from "./MenuOptions";

interface MobileMenuProps {
    isOpen: boolean;
    onClose: () => void;
}

export const MobileMenu = ({ isOpen, onClose }: MobileMenuProps) => {
    return (
        <Drawer
            anchor="left"
            open={isOpen}
            onClose={onClose}
            sx={{ pt: '3rem' }}
        >
            <MenuOptions />
        </Drawer>
    )
}