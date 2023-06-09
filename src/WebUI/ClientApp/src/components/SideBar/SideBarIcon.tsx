import { IconType } from "react-icons";

type SideBarIconPropsType = {
    icon: IconType;
    text?: String;
    active: boolean;
};

const SideBarIcon: React.FC<SideBarIconPropsType> = ({ icon: Icon, text, active }) => {
    return (
        <div className={`relative flex items-center justify-center h-12 w-12 mt-2 mb-2 mx-auto bg-gray-800
         text-green-500 hover:bg-green-600 hover:text-white rounded-3xl hover:rounded-xl transition-all duration-300 cursor-pointer ease-linear group
         ${active && 'bg-green-600 rounded-xl text-white'}`}>
            {<Icon />}

            {text && (
                <span className="absolute w-auto p-2 m-2 min-w-max left-14 rounded-md shadow-md text-white bg-gray-900 text-xs font-bold transition-all duration-100 scale-0 origin-left group-hover:scale-100">
                    {text}
                </span>
            )}
        </div>
    );
}

export default SideBarIcon;