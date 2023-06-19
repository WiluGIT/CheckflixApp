import { useContext, useEffect } from "react";
import useRefreshToken from "./useRefreshToken"
import AuthContext from "@/context/AuthContextProvider";
import { axiosApi } from "@/lib/api";
import { useNavigate } from "react-router-dom";
import { TOKENS_REFRESH_PATH } from "@/paths/api/authPaths";
import { ConsoleError } from "@/lib/interceptors";

const useAxiosApi = () => {
    const refresh = useRefreshToken();
    const { authState } = useContext(AuthContext);
    const navigate = useNavigate();

    useEffect(() => {
        const requestInterceptor = axiosApi.interceptors.request.use(
            config => {
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

                return Promise.reject(error);
            }
        );


        return () => {
            axiosApi.interceptors.request.eject(requestInterceptor);
            axiosApi.interceptors.response.eject(responseInterceptor);
        }
    }, [authState, refresh])

    return axiosApi;
}

export default useAxiosApi;