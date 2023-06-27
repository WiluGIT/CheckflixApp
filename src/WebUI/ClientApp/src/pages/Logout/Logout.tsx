import AuthContext from "@/context/AuthContextProvider";
import { useContext, useEffect } from "react";

const Logout = () => {
    const { globalLogOutDispatch } = useContext(AuthContext);

    useEffect(() => {
        globalLogOutDispatch();
    }, [globalLogOutDispatch]);

    return (
        <div className="flex justify-center items-center h-full">
            <span className="text-white loading loading-dots loading-lg"></span>
        </div>
    );
}

export default Logout;