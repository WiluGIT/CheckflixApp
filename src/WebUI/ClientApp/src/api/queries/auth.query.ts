import { LoginRequest, LoginResponse, RegisterRequest } from "@/types/auth";
import { UseMutationOptions, useMutation, useQuery } from "@tanstack/react-query";
import { login, register } from "../services/auth.service";
import { ServerError } from "@/types/api";

export const useLoginMutation = (
    config: UseMutationOptions<LoginResponse, ServerError, LoginRequest>
) => {
    return useMutation(
        ['login'],
        async (params: LoginRequest) => {
            return await login(params);
        },
        {
            ...config,
            onSuccess: (data, ...args) => {
                config?.onSuccess?.(data, ...args);
            },
        }
    );
};

export const useRegisterMutation = (
    config: UseMutationOptions<string, ServerError, RegisterRequest>
) => {
    return useMutation(
        ['register'],
        async (params: RegisterRequest) => {
            return await register(params);
        },
        {
            ...config
        }
    );
};