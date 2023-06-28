import SideBarIcon from "./SideBarIcon";
import { FaFire, FaHome } from 'react-icons/fa';
import { RiAdminFill, RiLoginBoxFill, RiLogoutCircleRLine } from 'react-icons/ri'
import { IconType } from "react-icons";
import { NavLink } from "react-router-dom";
import { useContext } from "react";
import AuthContext from "@/context/AuthContextProvider";

type NavItemsType = {
    label: string;
    icon: IconType;
    path: string;
    hideProtected: boolean;
};

const navItems: NavItemsType[] = [
    {
        label: "Home",
        icon: FaHome,
        path: "/",
        hideProtected: false
    },
    {
        label: "Admin",
        icon: RiAdminFill,
        path: "/admin",
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
        icon: FaFire,
        path: "/register",
        hideProtected: true
    }
]

const SideBar = () => {
    const { authState } = useContext(AuthContext);

    return (
        <nav className="w-[5.5rem] m-0 flex flex-col fixed h-full bg-[#202225] text-white shadow-lg">
            {navItems.map((item, idx) => (
                <NavLink to={item.path} key={idx}>
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