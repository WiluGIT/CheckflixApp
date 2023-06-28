import AuthContext from "@/context/AuthContextProvider";
import { UserData } from "@/types/auth";
import { useContext, useEffect } from "react";
import { useSearchParams } from "react-router-dom";

const AuthRedirect = () => {
    const { globalLogInDispatch } = useContext(AuthContext);
    const [searchParams, setSearchParams] = useSearchParams();

    useEffect(() => {
        const userData: UserData = {
            accessToken: searchParams.get("AccessToken") || '',
            id: searchParams.get("Id") || '',
            userName: searchParams.get("UserName") || '',
            email: searchParams.get("Email") || ''
        };

        globalLogInDispatch(userData);
    }, [globalLogInDispatch, searchParams]);

    return (
        <div className="flex justify-center items-center h-full">
            <span className="text-white loading loading-dots loading-lg"></span>
        </div>
    );
}

export default AuthRedirect;