import { has } from 'lodash';
import HomeIcon from '@mui/icons-material/Home'
import SettingsIcon from '@mui/icons-material/Settings';
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
    },
    'General': {
        path: '/General',
        icon: (
            <SettingsIcon />
        ),
        childrenRoutes: [
            {
                title: 'Horario',
                path: '/General/Horario',
                icon: null,
                associatedPageUrls: [
                    '/General/Horario/Nuevo',
                    '/General/Horario/:ScheduleId',
                ]
            },
            {
                title: 'Feriado',
                path: '/General/Feriado',
                icon: null,
                associatedPageUrls: [
                    '/General/Feriado/Nuevo',
                    '/General/Feriado/:HolidayId',
                ]
            },
            {
                title: 'Impuesto',
                path: '/General/Impuesto',
                icon: null,
                associatedPageUrls: [
                    '/General/Impuesto/Nuevo',
                    '/General/Impuesto/:taxId',
                ]
            },
            {
                title: 'Unidad medida',
                path: '/General/UnidadMedida',
                icon: null,
                associatedPageUrls: [
                    '/General/UnidadMedida/Nuevo',
                    '/General/UnidadMedida/:unitMeasureId',
                ]
            }
        ]
    }
}