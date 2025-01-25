import { getProtectedRoutes } from './ProtectedNavigation';
import { createBrowserRouter, createRoutesFromElements, RouterProvider } from 'react-router-dom';

const router = createBrowserRouter(
    createRoutesFromElements(
        <>
            {getProtectedRoutes()}
        </>
    )
)

export const Navigation = () => {
    return <RouterProvider router={router} />
};
