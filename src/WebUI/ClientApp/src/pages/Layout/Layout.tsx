import NavBar from "@/components/NavBar/NavBar";
import SideBar from "@/components/SideBar/SideBar";
import { FC } from "react";

type LayoutPropsType = {
    children: React.ReactNode;
};

const Layout: FC<LayoutPropsType> = ({ children }) => {
    return (
        <div className="flex w-full relative">
            <div className="absolute bg-main-gradient inset-0 max-h-screen -z-50"></div>
            <SideBar />
            <div className="flex-1 w-0 px-10 ml-[90px]">
                <NavBar />
                {children}
            </div>
        </div>
    );
};

export default Layout;