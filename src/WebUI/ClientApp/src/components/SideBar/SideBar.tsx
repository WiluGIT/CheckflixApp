import { useState } from "react";
import SideBarIcon from "./SideBarIcon";
import { FaFire } from 'react-icons/fa';
import { IconType } from "react-icons";

type NavItemsType = {
    label: string;
    icon: IconType;
};

const navItems: NavItemsType[] = [
    {
        label: "Home",
        icon: FaFire
    },
    {
        label: "Genres",
        icon: FaFire
    },
    {
        label: "New",
        icon: FaFire
    }
]

const SideBar = () => {
    const [activeIndex, setActiveIndex] = useState<number>(0);

    return (
        <div className="fixed top-0 left-0 h-screen w-16 m-0 flex flex-col bg-[#202225] text-white shadow-lg">
            {navItems.map((item, idx) => {
                return (
                    <div onClick={() => setActiveIndex(idx)}>
                        <SideBarIcon
                            key={idx}
                            text={item.label}
                            icon={item.icon}
                            active={idx === activeIndex} />
                    </div>
                );
            })}
        </div>
    );
}

export default SideBar;