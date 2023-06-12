import { LoginRequest, LoginResponse } from "@/types/auth";
import { useMutation, useQuery } from "@tanstack/react-query";
import { login } from "../services/auth.service";
import { AxiosError } from "axios";

export const useLoginMutation = () =>
    useMutation({
        mutationKey: ['login'],
        mutationFn: async (body: LoginRequest) => {
            const result = await login(body);
            return result;
        }
    });

