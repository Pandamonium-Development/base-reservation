import Cookies from 'js-cookie';
import { MobileMenu } from './MobileMenu';
import { MenuOptions } from './MenuOptions';
import { useLayout } from 'hooks/useLayout';
import { ToolbarIcon } from './ToolbarIcon';
import { useState, MouseEvent } from 'react';
import { useAuth } from 'contexts/AuthContext';
import MenuIcon from '@mui/icons-material/Menu';
import { AppBar, Avatar, Box, Divider, IconButton, Menu, MenuItem, Toolbar, Typography } from '@mui/material';

export const NavHeader = () => {
    const { isMobile } = useLayout();
    const { logout } = useAuth();
    const userName = Cookies.get('user_name');

    const [menuMobileOpen, setMenuMobileOpen] = useState(false);
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const [openMenu, setOpenMenu] = useState(false);

    const toggleMenuMobile = (state: boolean) => setMenuMobileOpen(state);

    const handleAvatarClick = (event: MouseEvent<HTMLDivElement>) => {
        setAnchorEl(event.currentTarget);
        setOpenMenu(true);
    };

    const handleCloseMenu = () => {
        setOpenMenu(false);
    };

    const handleLogout = () => {
        logout();
        handleCloseMenu();
    };

    return (
        <AppBar
            position="fixed"
            sx={{
                top: 15,
                left: 0,
                right: 0,
                borderRadius: '32px',
                backgroundColor: 'primary.main',
                padding: '8px',
                zIndex: 1200,
                display: 'flex',
                justifyContent: 'center',
                margin: '0 auto',
                width: '90%'
            }}
        >
            <Toolbar
                sx={{
                    display: 'flex',
                    justifyContent: 'space-between',
                    alignItems: 'center',
                    borderRadius: '32px',
                }}
            >
                {isMobile && (
                    <Box sx={{ display: 'flex', alignItems: 'center' }}>
                        <IconButton color="inherit" aria-label="menu" onClick={() => toggleMenuMobile(true)}>
                            <MenuIcon sx={{ fontSize: '32px' }} />
                        </IconButton>
                    </Box>
                )}
                <MobileMenu isOpen={menuMobileOpen} onClose={() => toggleMenuMobile(false)} />

                <Box sx={{ display: isMobile ? 'none' : 'flex', alignItems: 'center', gap: 5 }}>
                    <MenuOptions />
                </Box>

                <Box sx={{ display: 'flex', justifyContent: 'center', flexGrow: 1, width: '100%' }}>
                    <ToolbarIcon />
                </Box>

                <Box sx={{ display: 'flex', alignItems: 'center' }}>
                    {!isMobile && (<Typography variant='body2' sx={{ minWidth: '9rem' }}>{userName}</Typography>)}
                    <Avatar
                        onClick={handleAvatarClick} // Al hacer clic, abre el menú desplegable
                        sx={{ cursor: 'pointer' }}
                    />
                    <Menu
                        anchorEl={anchorEl}
                        open={openMenu}
                        onClose={handleCloseMenu}
                        anchorOrigin={{
                            vertical: 'bottom',
                            horizontal: 'center',
                        }}
                        transformOrigin={{
                            vertical: 'top',
                            horizontal: 'center',
                        }}
                    >
                        <MenuItem sx={{ paddingBottom: '2rem' }}>
                            <Typography variant='body2'>{userName}</Typography>
                        </MenuItem>
                        <Divider />
                        <MenuItem onClick={handleLogout}>Cerrar sesión</MenuItem>
                    </Menu>
                </Box>
            </Toolbar>
        </AppBar>
    )
}