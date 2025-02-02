import { has } from 'lodash';
import HomeIcon from '@mui/icons-material/Home'
import HolidayVillageIcon from '@mui/icons-material/HolidayVillage';
import { type NavBarRouteDef, type NavBarDefWithChildren } from 'types/nav'

export const isChildrenRouteDef = (
    x: NavBarRouteDef
): x is NavBarDefWithChildren => {
    return has(x, 'childrenRoutes')
}

export const NavBarRoutes: Record<string, NavBarRouteDef> = {
    Inicio: {
        path: '/Inicio',
        icon: (
            <HomeIcon />
        ),
    },
    Sucursal: {
        path: '/Sucursal',
        icon: (
            <HolidayVillageIcon />
        ),
    }
}