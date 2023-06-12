import { api } from "@/lib/api";
import { LoginRequest, LoginResponse } from "@/types/auth";

const login = async (params: LoginRequest): Promise<LoginResponse> => {
    const { data } = await api.post<LoginResponse>('/Tokens', { ...params });
    return data;
};


// export const login = async (body: LoginBody) => {
//     const res = new Promise<boolean>((resolve, reject) => {
//         if (body.username !== 'user' || body.password !== 'user') {
//             reject(new Error('Invalid username or password'));
//         }

//         setTimeout(() => {
//             resolve(true);
//         }, 2000);
//     });
//     return await res;
// };

export { login };