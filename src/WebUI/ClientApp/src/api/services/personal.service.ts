import { axiosApi } from "@/lib/api";
import { GET_PROFILE_PATH } from "@/paths/api/profilePaths";
import { GetProfileQueryRequest, GetProfileQueryResponse } from "@/types/profile";

const getProfile = async (params: GetProfileQueryRequest): Promise<GetProfileQueryResponse> => {
    const { data } = await axiosApi.get<GetProfileQueryResponse>(
        GET_PROFILE_PATH,
        { params: { ...params } })
    return data;
}


export { getProfile }