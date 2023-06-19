import { LoginRequest, LoginResponse, RegisterRequest } from "@/types/auth";
import { UseMutationOptions, useMutation } from "@tanstack/react-query";
import { login, register } from "../services/auth.service";
import { ServerError } from "@/types/api";
import { AxiosInstance } from "axios";

export const useLoginMutation = (
    config: UseMutationOptions<LoginResponse, ServerError, LoginRequest>,
    axiosInstance?: AxiosInstance,
) => {
    return useMutation(
        ['login'],
        async (params: LoginRequest) => {
            return await login(params, axiosInstance);
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
    config: UseMutationOptions<string, ServerError, RegisterRequest>,
    axiosInstance?: AxiosInstance,
) => {
    return useMutation(
        ['register'],
        async (params: RegisterRequest) => {
            return await register(params, axiosInstance);
        },
        {
            ...config
        }
    );
};