import React, { FC, useContext, useEffect, useState } from "react";
import useRefreshToken from "../hooks/useRefreshToken"
import AuthContext from "@/context/AuthContextProvider";
import { axiosApi } from "@/lib/api";
import { useNavigate } from "react-router-dom";
import { ConsoleError } from "@/lib/interceptors";
import { TOKENS_REFRESH_PATH } from "@/paths/api/authPaths";

type AxiosApiInitializerType = {
    children: React.ReactNode;
}

const AxiosApiInitializer: FC<AxiosApiInitializerType> = ({ children }) => {
    const refresh = useRefreshToken();
    const { authState } = useContext(AuthContext);
    const navigate = useNavigate();
    const [isSet, setIsSet] = useState(false)

    useEffect(() => {
        const requestInterceptor = axiosApi.interceptors.request.use(
            config => {
                debugger;
                if (!config.headers['Authorization'] && authState?.user?.accessToken) {
                    config.headers['Authorization'] = `Bearer ${authState?.user?.accessToken}`;
                }

                return config;
            }, (error) => Promise.reject(error)
        );

        const responseInterceptor = axiosApi.interceptors.response.use(
            response => response,
            async (error) => {
                const prevRequest = error?.config;
                debugger;
                if (error?.response?.status === 401) {
                    if (prevRequest.url.includes(TOKENS_REFRESH_PATH)) {
                        navigate('/login');
                        return Promise.reject(error);
                    }

                    const newAccessToken = await refresh();
                    prevRequest.headers['Authorization'] = `Bearer ${newAccessToken}`;

                    return axiosApi(prevRequest);
                }

                if (error.response) {
                    const errorMessage: ConsoleError = {
                        status: error.response.status,
                        data: error.response.data,
                    };
                    console.error(errorMessage);
                } else if (error.request) {
                    console.error(error.request);
                } else {
                    console.error('Error', error.message);
                }
                await Promise.reject(error);
            }
        );

        setIsSet(true);
        return () => {
            setIsSet(false);
            axiosApi.interceptors.request.eject(requestInterceptor);
            axiosApi.interceptors.response.eject(responseInterceptor);
        }
    }, [authState, refresh])

    return isSet && children;
}

export default AxiosApiInitializer;