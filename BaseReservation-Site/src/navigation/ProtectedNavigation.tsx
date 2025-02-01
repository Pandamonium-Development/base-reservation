import { Home } from "pages/Home/Home";
import { Route } from "react-router-dom";
import { Branch } from "pages/Branch/Branch";
import { BranchNewEditWrapper } from "pages/Branch/BranchNewEditWrapper";

export const routesProtected = [
    {
        name: 'Inicio',
        path: '/Inicio',
        showInMenu: true,
        element: <Home />
    },
    {
        name: 'Sucursal',
        path: '/Sucursal',
        showInMenu: true,
        element: <Branch />
    },
    {
        name: 'Crear Sucursal',
        showInMenu: false,
        path: '/Sucursal/Nueva',
        element: <BranchNewEditWrapper />
    },
    {
        name: 'Editar Sucursal',
        showInMenu: false,
        path: '/Sucursal/:id',
        element: <BranchNewEditWrapper />
    }
]

export const getProtectedRoutes = () =>
    routesProtected.map((route) => (
        <Route path={route.path} key={route.path} element={route.element} />
    ))