import { refreshToken } from "@/api/services/auth.service";
import AuthContext from "@/context/AuthContextProvider";
import { axiosApi } from "@/lib/api";
import { UserData } from "@/types/auth";
import { useContext } from "react";

const useRefreshToken = () => {
    const { authState, globalLogInDispatch } = useContext(AuthContext);

    const refresh = async () => {
        const { token } = await refreshToken({ token: authState?.user?.accessToken as string });
        const userData: UserData = {
            accessToken: token,
            email: authState?.user?.email || '',
            userName: authState?.user?.userName || '',
            id: authState?.user?.id || ''
        };
        globalLogInDispatch(userData, false);

        return token;
    }

    return refresh;
}

export default useRefreshToken;