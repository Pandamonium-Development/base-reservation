import { Home } from "pages/Home/Home";
import { Route } from "react-router-dom";
import { Branch } from "pages/Branch/Branch";
import { BranchNewEdit } from "pages/Branch/BranchNewEdit";

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
        name: '',
        path: '/Sucursal/Nueva',
        element: <BranchNewEdit />
    }
]

export const getProtectedRoutes = () =>
    routesProtected.map((route) => (
        <Route path={route.path} key={route.path} element={route.element} />
    ))