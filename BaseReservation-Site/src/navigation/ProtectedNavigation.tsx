import { Home } from "pages/Home/Home";
import { Route } from "react-router-dom";
import { Branch } from "pages/Branch/Branch";
import { Tax } from "pages/Settings/Tax/Tax";
import { Holiday } from "pages/Settings/Holiday/Holiday";
import { Schedule } from "pages/Settings/Schedule/Schedule";
import { UnitMeasure } from "pages/Settings/UnitMeasure/UnitMeasure";
import { BranchNewEditWrapper } from "pages/Branch/BranchNewEditWrapper";
import { TaxNewEditWrapper } from "pages/Settings/Tax/TaxNewEditWrapper";
import { Schedule as BranchSchedule } from "pages/Branch/Schedule/Schedule";
import { Block as BranchScheduleBlock } from "pages/Branch/Schedule/Block/Block";
import { HolidayNewEditWrapper } from "pages/Settings/Holiday/HolidayNewEditWrapper";
import { ScheduleNewEditWrapper } from "pages/Settings/Schedule/ScheduleNewEditWrapper";
import { UnitMeasureNewEditWrapper } from "pages/Settings/UnitMeasure/UnitMeasureNewEditWrapper";
import { ScheduleManagement as BranchScheduleManagement } from "pages/Branch/Schedule/ScheduleManagement";
import { BlockNewEditWrapper as BranchScheduleBlockNewEditWrapper } from "pages/Branch/Schedule/Block/BlockNewEditWrapper";

export const routesProtected = [
    {
        name: 'Inicio',
        path: '/Inicio',
        element: <Home />
    },
    {
        name: 'Sucursal',
        path: '/Sucursal',
        element: <Branch />
    },
    {
        name: 'CrearSucursal',
        path: '/Sucursal/Nueva',
        element: <BranchNewEditWrapper />
    },
    {
        name: 'EditarSucursal',
        path: '/Sucursal/:branchId',
        element: <BranchNewEditWrapper />
    },
    {
        name: 'Horario',
        path: '/General/Horario',
        element: <Schedule />
    },
    {
        name: 'CrearHorario',
        path: '/General/Horario/Nuevo',
        element: <ScheduleNewEditWrapper />
    },
    {
        name: 'EditarHorario',
        path: '/General/Horario/:scheduleId',
        element: <ScheduleNewEditWrapper />
    },
    {
        name: 'SucursalHorarios',
        path: '/Sucursal/:branchId/Horario',
        element: <BranchSchedule />
    },
    {
        name: 'SucursalHorariosGestion',
        path: '/Sucursal/:branchId/Horario/Gestion',
        element: <BranchScheduleManagement />
    },
    {
        name: 'SucursalHorarioBloqueos',
        path: '/Sucursal/:branchId/Horario/:scheduleId/Bloqueo',
        element: <BranchScheduleBlock />
    },
    {
        name: 'CrearSucursalHorarioBloqueo',
        path: '/Sucursal/:branchId/Horario/:scheduleId/Bloqueo/Nuevo',
        element: <BranchScheduleBlockNewEditWrapper />
    },
    {
        name: 'EditarSucursalHorarioBloqueo',
        path: '/Sucursal/:branchId/Horario/:scheduleId/Bloqueo/:blockId',
        element: <BranchScheduleBlockNewEditWrapper />
    },
    {
        name: 'Feriado',
        path: '/General/Feriado',
        element: <Holiday />
    },
    {
        name: 'CrearFeriado',
        path: '/General/Feriado/Nuevo',
        element: <HolidayNewEditWrapper />
    },
    {
        name: 'EditarFeriado',
        path: '/General/Feriado/:holidayId',
        element: <HolidayNewEditWrapper />
    },
    {
        name: 'Impuesto',
        path: '/General/Impuesto',
        element: <Tax />
    },
    {
        name: 'CrearImpuesto',
        path: '/General/Impuesto/Nuevo',
        element: <TaxNewEditWrapper />
    },
    {
        name: 'EditarImpuesto',
        path: '/General/Impuesto/:taxId',
        element: <TaxNewEditWrapper />
    },
    {
        name: 'UnidadMedida',
        path: '/General/UnidadMedida',
        element: <UnitMeasure />
    },
    {
        name: 'CrearUnidadMedida',
        path: '/General/UnidadMedida/Nuevo',
        element: <UnitMeasureNewEditWrapper />
    },
    {
        name: 'EditarUnidadMedida',
        path: '/General/UnidadMedida/:unitMeasureId',
        element: <UnitMeasureNewEditWrapper />
    }
]

export const getProtectedRoutes = () =>
    routesProtected.map((route) => (
        <Route path={route.path} key={route.path} element={route.element} />
    ))