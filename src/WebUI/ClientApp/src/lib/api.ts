import axios, { type AxiosInstance, type AxiosRequestConfig } from 'axios';
import {
    errorInterceptor,
    requestInterceptor,
    successInterceptor,
} from './interceptors';

const axiosRequestConfig: AxiosRequestConfig = {
    baseURL: import.meta.env.VITE_BASE_URL,
    responseType: 'json',
    headers: {
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
    }
};

const axiosApi: AxiosInstance = axios.create(axiosRequestConfig);


//axiosApi.interceptors.request.use(requestInterceptor);
//axiosApi.interceptors.response.use(successInterceptor, errorInterceptor);

export { axiosApi };
