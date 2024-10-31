import MainLayout from "@/app/layout";
import Welcome from "@/app/welcome";
import Packages from "@/app/packages";
import { createBrowserRouter, RouteObject } from "react-router-dom";
import LoginPage from "@/app/login";
import Upload from "@/app/upload";


const routes = [
    {
        element: <MainLayout/>,
        children: [
            {
                element:<Welcome/>,
                path: "/",
            },
            {
                element: <Packages/>,
                path: "/packages",
            },
            {
                element: <Upload/>,
                path: "/upload",
            }
        ],
    },
    {
        path: "/login",
        element: <LoginPage/>,
    }
] as RouteObject[];

const browserRoutes = createBrowserRouter(routes);

export default browserRoutes;