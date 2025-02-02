import { Home } from "pages/Home/Home";
import { Route } from "react-router-dom";
import { Branch } from "pages/Branch/Branch";
import { BranchNewEditWrapper } from "pages/Branch/BranchNewEditWrapper";
import { Schedule } from "pages/Branch/Schedule/Schedule";

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
        name: 'Crear Sucursal',
        path: '/Sucursal/Nueva',
        element: <BranchNewEditWrapper />
    },
    {
        name: 'Editar Sucursal',
        path: '/Sucursal/:id',
        element: <BranchNewEditWrapper />
    },
    {
        name: 'SucursalHorarios',
        path: '/Sucursal/:id/Horarios',
        element: <Schedule />
    }
]

export const getProtectedRoutes = () =>
    routesProtected.map((route) => (
        <Route path={route.path} key={route.path} element={route.element} />
    ))