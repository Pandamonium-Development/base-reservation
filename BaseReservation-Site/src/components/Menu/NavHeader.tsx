import { MenuOptions } from './MenuOptions';
import { useLayout } from 'hooks/useLayout';
import { ToolbarIcon } from './ToolbarIcon';
import MenuIcon from '@mui/icons-material/Menu';
import { AppBar, Avatar, Box, IconButton, Toolbar } from '@mui/material';
import { MobileMenu } from './MobileMenu';
import { useState } from 'react';

export const NavHeader = () => {
    const { isMobile } = useLayout();

    const [menuMobileOpen, setMenuMobileOpen] = useState(false);

    const toggleMenuMobile = (state: boolean) => setMenuMobileOpen(state);

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
                    <Avatar />
                </Box>
            </Toolbar>
        </AppBar>
    )
}