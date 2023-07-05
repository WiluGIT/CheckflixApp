import { axiosApi } from "@/lib/api";
import { GET_USER_COLLECTIONS_COUNT_PATH } from "@/paths/api/userCollectionPaths";
import { GetUserCollectionsRequest, GetUserCollectionsResponse } from "@/types/userCollections";

const getUserCollections = async (params: GetUserCollectionsRequest): Promise<GetUserCollectionsResponse> => {
    const { data } = await axiosApi.get<GetUserCollectionsResponse>(
        GET_USER_COLLECTIONS_COUNT_PATH,
        { params: { ...params } })
    return data;
}


export { getUserCollections }