import { BrowserRouter, Route, RouteObject, Routes, useRoutes } from 'react-router-dom';
import Home from '@/pages/Home/Home'
import Admin from '@/pages/Admin/Admin';
import Login from '@/pages/Login/Login';
import WrapperRouteComponent from './config';
import Register from '@/pages/Register/Register';
import Logout from '@/pages/Logout/Logout';
import AuthRedirect from '@/pages/Login/AuthRedirect';


// const NotFound = lazy(() => import('@/pages/404'));
// const Project = lazy(() => import('@/pages/project'));

const routeList: RouteObject[] = [
    {
        path: "/",
        element: <WrapperRouteComponent auth={false}><Home /></WrapperRouteComponent>,
    },
    {
        path: "admin",
        element: <WrapperRouteComponent auth={false}><Admin /></WrapperRouteComponent>,
    },
    {
        path: "login",
        element: <WrapperRouteComponent auth={false}><Login /></WrapperRouteComponent>,
    },
    {
        path: "register",
        element: <WrapperRouteComponent auth={false}><Register /></WrapperRouteComponent>,
    },
    {
        path: "logout",
        element: <WrapperRouteComponent auth={false}><Logout /></WrapperRouteComponent>,
    },
    {
        path: "authRedirect",
        element: <WrapperRouteComponent auth={false}><AuthRedirect /></WrapperRouteComponent>,
    },
];

const Router = () => {
    const element = useRoutes(routeList);
    return element;
};

export default Router;
