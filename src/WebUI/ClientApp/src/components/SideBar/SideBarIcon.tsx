import { IconType } from "react-icons";

type SideBarIconPropsType = {
    icon: IconType;
    text?: String;
    active: boolean;
};

const SideBarIcon: React.FC<SideBarIconPropsType> = ({ icon: Icon, text, active }) => {
    return (
        <div className={`relative flex flex-col items-center justify-center h-[70px] w-[70px] mt-2 mb-2 mx-auto text-white 
         hover:bg-[#0a2c20] hover:rounded-[12px] transition-all duration-300 cursor-pointer ease-linear group
         ${active ? 'rounded-[12px] bg-[#0a2c20] ' : 'bg-[#4b4b4f] rounded-[40px]'}`}>
            <Icon className="text-xl" />
            <span className="text-xs mt-1">{text}</span>
        </div>
    );
}

export default SideBarIcon;