import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Typography, Menu, MenuItem, ListItemText, Box } from '@mui/material';

interface Option {
    Name: string,
    Route: string
}

export const ToolbarOptionList = ({ OptionName, Options }: { OptionName: string, Options: Option[] }) => {
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const navigate = useNavigate();

    const openMenu = (event: React.MouseEvent<HTMLElement>) => setAnchorEl(event.currentTarget);
    const closeMenu = () => setAnchorEl(null);

    const handleMenuItemClick = (route: string) => {
        navigate(route);
        closeMenu();
    };

    return (
        <Box>
            <Typography
                variant="body2"
                sx={{ color: 'text.primary', cursor: 'pointer' }}
                onClick={openMenu}
            >
                {OptionName}
            </Typography>

            <Menu
                anchorEl={anchorEl}
                open={Boolean(anchorEl)}
                onClose={closeMenu}
            >
                {Options.map((option, index) => (
                    <MenuItem key={`${option.Name}-${index}`} onClick={() => handleMenuItemClick(option.Route)}>
                        <ListItemText primary={option.Name} />
                    </MenuItem>
                ))}
            </Menu>
        </Box >
    );
};