import { axiosApi } from "@/lib/api";
import { TOKENS_PATH, TOKENS_REFRESH_PATH, USERS_PATH } from "@/paths/api/authPaths";
import { LoginRequest, LoginResponse, RefreshTokenRequest, RefreshTokenResponse, RegisterRequest } from "@/types/auth";
import { AxiosInstance } from "axios";

const login = async (params: LoginRequest, axiosInstance: AxiosInstance = axiosApi): Promise<LoginResponse> => {
    const { data } = await axiosInstance.post<LoginResponse>(TOKENS_PATH,
        { ...params },
        { withCredentials: true });
    return data;
};

const register = async (params: RegisterRequest, axiosInstance: AxiosInstance = axiosApi): Promise<string> => {
    const { data } = await axiosInstance.post<string>(USERS_PATH,
        { ...params },
        { withCredentials: true });
    return data;
};

const refreshToken = async (params: RefreshTokenRequest, axiosInstance: AxiosInstance = axiosApi): Promise<RefreshTokenResponse> => {
    const { data } = await axiosInstance.post<RefreshTokenResponse>(TOKENS_REFRESH_PATH,
        { token: params.token },
        { withCredentials: true });

    return data;
}

export { login, register, refreshToken };