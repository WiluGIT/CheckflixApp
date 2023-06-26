import SideBar from "@/components/SideBar/SideBar";
import { FC } from "react";

type LayoutPropsType = {
    children: React.ReactNode;
};

const Layout: FC<LayoutPropsType> = ({ children }) => {
    return (
        <div className="flex w-full min-h-screen bg-color-gradient bg-main-gradient">
            <SideBar />
            <div className="w-[80%] mx-auto">
                {children}
            </div>
        </div>
    );
};

export default Layout;