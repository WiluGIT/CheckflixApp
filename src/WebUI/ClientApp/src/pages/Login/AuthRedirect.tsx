import { getProfile } from "@/api/services/personal.service";
import AuthContext from "@/context/AuthContextProvider";
import { UserData } from "@/types/auth";
import { GetProfileQueryRequest } from "@/types/profile";
import { useContext, useEffect } from "react";
import { useSearchParams } from "react-router-dom";

const AuthRedirect = () => {
    const { globalLogInDispatch } = useContext(AuthContext);
    const [searchParams, setSearchParams] = useSearchParams();

    useEffect(() => {
        const getProfileData = async () => {
            const params: GetProfileQueryRequest = {
                accessToken: searchParams.get("AccessToken") || ''
            };

            const data = await getProfile(params);
            const userData: UserData = {
                accessToken: searchParams.get("AccessToken") || '',
                id: data?.id || '',
                userName: data?.userName || '',
                email: data?.email || '',
                imageUrl: data?.imageUrl || '',
                roles: data?.roles || []
            };

            globalLogInDispatch(userData);
        }

        getProfileData();
    }, [globalLogInDispatch, searchParams]);

    return (
        <div className="flex justify-center items-center h-full">
            <span className="text-white loading loading-dots loading-lg"></span>
        </div>
    );
}

export default AuthRedirect;