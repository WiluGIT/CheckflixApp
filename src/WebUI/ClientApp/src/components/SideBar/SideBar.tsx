import SideBarIcon from "./SideBarIcon";
import { FaFire, FaHome } from 'react-icons/fa';
import { RiAdminFill, RiLoginBoxFill } from 'react-icons/ri'
import { IconType } from "react-icons";
import { NavLink } from "react-router-dom";

type NavItemsType = {
    label: string;
    icon: IconType;
    path: string;
};

const navItems: NavItemsType[] = [
    {
        label: "Home",
        icon: FaHome,
        path: "/"
    },
    {
        label: "Admin",
        icon: RiAdminFill,
        path: "/admin"
    },
    {
        label: "Login",
        icon: RiLoginBoxFill,
        path: "/login"
    },
    {
        label: "Register",
        icon: FaFire,
        path: "/register"
    }
]

const SideBar = () => {
    return (
        <nav className="w-20 m-0 flex flex-col bg-[#202225] text-white shadow-lg">
            {navItems.map((item, idx) => (
                <NavLink to={item.path} key={idx}>
                    {({ isActive }) => (
                        <SideBarIcon
                            key={idx}
                            text={item.label}
                            icon={item.icon}
                            active={isActive} />
                    )}
                </NavLink>
            ))}
        </nav>
    );
}

export default SideBar;