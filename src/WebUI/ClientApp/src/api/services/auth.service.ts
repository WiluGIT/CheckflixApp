import { api } from "@/lib/api";
import { LoginRequest, LoginResponse, RegisterRequest } from "@/types/auth";

const login = async (params: LoginRequest): Promise<LoginResponse> => {
    const { data } = await api.post<LoginResponse>('/Tokens', { ...params });
    return data;
};

const register = async (params: RegisterRequest): Promise<string> => {
    const { data } = await api.post<string>('/Users', { ...params });
    return data;
};

export { login, register };