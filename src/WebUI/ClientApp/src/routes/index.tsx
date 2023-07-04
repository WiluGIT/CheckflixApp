import { BrowserRouter, Route, RouteObject, Routes, useRoutes } from 'react-router-dom';
import Home from '@/pages/Home/Home'
import Admin from '@/pages/Admin/Admin';
import Login from '@/pages/Login/Login';
import WrapperRouteComponent from './config';
import Register from '@/pages/Register/Register';
import Logout from '@/pages/Logout/Logout';
import AuthRedirect from '@/pages/Login/AuthRedirect';
import Search from '@/pages/Search/Search';
import News from '@/pages/News/News';
import Genres from '@/pages/Genres/Genres';
import People from '@/pages/People/People';


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
    {
        path: "search",
        element: <WrapperRouteComponent auth={false}><Search /></WrapperRouteComponent>,
    },
    {
        path: "news",
        element: <WrapperRouteComponent auth={false}><News /></WrapperRouteComponent>,
    },
    {
        path: "genres",
        element: <WrapperRouteComponent auth={false}><Genres /></WrapperRouteComponent>,
    },
    {
        path: "people",
        element: <WrapperRouteComponent auth={false}><People /></WrapperRouteComponent>,
    },
];

const Router = () => {
    const element = useRoutes(routeList);
    return element;
};

export default Router;
