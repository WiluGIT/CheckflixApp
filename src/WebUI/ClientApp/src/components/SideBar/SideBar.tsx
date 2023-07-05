import SideBarIcon from "./SideBarIcon";
import { FaFire, FaHome } from 'react-icons/fa';
import { RiAdminFill, RiLoginBoxFill, RiLogoutCircleRLine } from 'react-icons/ri';
import { BsPeopleFill, BsSignIntersection } from 'react-icons/bs';
import { SlArrowUp } from 'react-icons/sl';
import { BiCategoryAlt } from 'react-icons/bi';
import { IconType } from "react-icons";
import { NavLink } from "react-router-dom";
import { useContext } from "react";
import AuthContext from "@/context/AuthContextProvider";
import { logo, logoWhite } from "@/assets";

type NavItemsType = {
    label: string;
    icon: IconType;
    path: string;
    hideProtected: boolean;
    isAdminRoute?: boolean;
};

const navItems: NavItemsType[] = [
    {
        label: "Home",
        icon: FaHome,
        path: "/",
        hideProtected: false
    },
    {
        label: "News",
        icon: FaFire,
        path: "/news",
        hideProtected: false
    },
    {
        label: "Genres",
        icon: BiCategoryAlt,
        path: "/genres",
        hideProtected: false
    },
    {
        label: "People",
        icon: BsPeopleFill,
        path: "/people",
        hideProtected: false
    },
    {
        label: "Login",
        icon: RiLoginBoxFill,
        path: "/login",
        hideProtected: true
    },
    {
        label: "Register",
        icon: BsSignIntersection,
        path: "/register",
        hideProtected: true
    }
]

const SideBar = () => {
    const { authState } = useContext(AuthContext);
    const isAdmin = true;

    return (
        <nav className="min-w-[90px] m-0 flex flex-col bg-[#202225] text-white shadow-lg fixed h-full">
            <NavLink to={'/'}>
                <div className="flex w-full h-[90px] py-1 px-2 mb-2 mt-2">
                    <img src={logoWhite} />
                </div>
            </NavLink>

            {authState.isAuthenticated && (
                <div className="avatar flex flex-col justify-center items-center">
                    <NavLink to={`/profile/${authState.user?.id}`}>
                        {({ isActive }) => {
                            return <div className={`transition-all duration-300 ease-linear ${isActive ? "ring-primary" : "ring-accent"} w-14 hover:ring-primary rounded-box ring  ring-offset-base-100 ring-offset-2 mt-5 overflow-hidden`}>
                                <img src="http://daisyui.com/tailwind-css-component-profile-3@56w.png" />
                            </div>
                        }}
                    </NavLink>
                    <div className="w-[90%] h-10 text-7xl">
                        <div className="divider"><SlArrowUp /></div>
                    </div>
                </div>
            )}

            {navItems.map((item, idx) => (
                <NavLink to={item.path} key={idx} className={({ isActive, isPending }) =>
                    isPending ? "pending" : isActive ? "active" : ""
                } >
                    {({ isActive }) => {
                        if (item.hideProtected && authState.isAuthenticated) return;

                        return <SideBarIcon
                            key={idx}
                            text={item.label}
                            icon={item.icon}
                            active={isActive} />
                    }}
                </NavLink>
            ))}

            {authState.isAuthenticated && isAdmin &&
                <NavLink to={'/admin'}>
                    {({ isActive }) => {
                        return <SideBarIcon
                            text={'Admin'}
                            icon={RiAdminFill}
                            active={isActive} />
                    }}
                </NavLink>
            }

            {authState.isAuthenticated &&
                <NavLink to={'/logout'}>
                    <SideBarIcon
                        text={'Log out'}
                        icon={RiLogoutCircleRLine}
                        active={false} />
                </NavLink>
            }
        </nav>
    );
}

export default SideBar;