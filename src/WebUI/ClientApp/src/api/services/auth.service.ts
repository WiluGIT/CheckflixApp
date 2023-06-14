import { axiosApi } from "@/lib/api";
import { LoginRequest, LoginResponse, RefreshTokenRequest, RefreshTokenResponse, RegisterRequest } from "@/types/auth";

const login = async (params: LoginRequest): Promise<LoginResponse> => {
    const { data } = await axiosApi.post<LoginResponse>('/Tokens',
        { ...params },
        { withCredentials: true });
    return data;
};

const register = async (params: RegisterRequest): Promise<string> => {
    const { data } = await axiosApi.post<string>('/Users',
        { ...params },
        { withCredentials: true });
    return data;
};

const refreshToken = async (params: RefreshTokenRequest): Promise<RefreshTokenResponse> => {
    const { data } = await axiosApi.post<RefreshTokenResponse>('/Tokens/refresh',
        { token: params.token },
        { withCredentials: true });

    return data;
}

export { login, register, refreshToken };