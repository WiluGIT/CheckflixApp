import { IconType } from "react-icons";

type SideBarIconPropsType = {
    icon: IconType;
    text?: String;
    active: boolean;
};

const SideBarIcon: React.FC<SideBarIconPropsType> = ({ icon: Icon, text, active }) => {
    return (
        <div className={`relative flex flex-col items-center justify-center h-[70px] w-[70px] mt-2 mb-2 mx-auto bg-gray-800
         text-white hover:bg-[#4a3559] hover:text-white rounded-[40px] hover:rounded-xl transition-all duration-300 cursor-pointer ease-linear group
         ${active && 'bg-[#4a3559] rounded-[12px] text-white'}`}>
            <Icon className="text-xl" />
            <span className="text-xs mt-1">{text}</span>
        </div>
    );
}

export default SideBarIcon;