import { type ReactNode } from 'react'

export type NavRoutes =
    '/Inicio' |
    '/Sucursal' |
    '/General' |
    '/General/Horario' |
    '/General/Feriado' |
    '/General/Impuesto' |
    '/General/UnidadMedida';

export interface NavBarDef {
    path: NavRoutes
    icon: ReactNode
}

export type ChildrenRouteDef = Array<
    NavBarDef & { title: string, associatedPageUrls: string[] }
>

export interface NavBarDefWithChildren {
    icon: ReactNode,
    childrenRoutes: ChildrenRouteDef
}

export type NavBarRouteDef = NavBarDef | NavBarDefWithChildren