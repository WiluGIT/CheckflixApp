import { axiosApi } from "@/lib/api";
import { GET_PRODUCTIONS_PATH } from "@/paths/api/productionPaths";
import { GetProductionsRequest, GetProductionsResponse } from "@/types/production";
import { PaginationResponse } from "@/types/requests";
import { AxiosInstance } from "axios";

const getProductions = async (params: GetProductionsRequest, axiosInstance: AxiosInstance = axiosApi): Promise<PaginationResponse<GetProductionsResponse>> => {
    const { data } = await axiosInstance.get<PaginationResponse<GetProductionsResponse>>(
        GET_PRODUCTIONS_PATH,
        { params: { ...params } });
    return data;
};

export { getProductions };