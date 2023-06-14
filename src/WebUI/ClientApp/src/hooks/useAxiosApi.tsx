import { useContext, useEffect, useState } from "react";
import useRefreshToken from "./useRefreshToken"
import AuthContext from "@/context/AuthContextProvider";
import { axiosApi } from "@/lib/api";
import { useNavigate } from "react-router-dom";

const useAxiosApi = () => {
    const refresh = useRefreshToken();
    const { authState } = useContext(AuthContext);
    const navigate = useNavigate();
    const [counter, setCounter] = useState(0);

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
                debugger;
                const prevRequest = error?.config;
                if (error?.response?.status === 401) {
                    if (prevRequest.url.includes("/Tokens/refresh")) {
                        navigate('/login');
                        return Promise.reject(error);
                    }

                    const newAccessToken = await refresh();
                    prevRequest.headers['Authorization'] = `Bearer ${newAccessToken}`;

                    return axiosApi(prevRequest);
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