import { axiosApi } from "@/lib/api";
import { FOLLOW_USER_PATH, GET_FOLLOWERS_PATH, GET_FOLLOWING_COUNT_PATH, GET_FOLLOWING_PATH, GET_USERS_WITH_FOLLOWING_PATH, UNFOLLOW_USER_PATH } from "@/paths/api/followingPaths";
import { GetFollowingCountRequest, GetFollowingCountResponse, GetFollowingRequest, GetFollowingResponse, GetUsersWithFollowingRequest, GetUsersWithFollowingResponse } from "@/types/following";

const getUsersWithFollowing = async (params: GetUsersWithFollowingRequest): Promise<GetUsersWithFollowingResponse[]> => {
    const { data } = await axiosApi.get<GetUsersWithFollowingResponse[]>(
        GET_USERS_WITH_FOLLOWING_PATH,
        { params: { ...params } })
    return data;
};

const follow = async (userId: string): Promise<string> => {
    const { data } = await axiosApi.post<string>(FOLLOW_USER_PATH(userId),
        { withCredentials: true });

    return data;
};

const unFollow = async (userId: string): Promise<string> => {
    const { data } = await axiosApi.delete<string>(UNFOLLOW_USER_PATH(userId),
        { withCredentials: true });

    return data;
};

const getFollowingCount = async (params: GetFollowingCountRequest): Promise<GetFollowingCountResponse> => {
    const { data } = await axiosApi.get<GetFollowingCountResponse>(
        GET_FOLLOWING_COUNT_PATH,
        { params: { ...params } })
    return data;
}

const getFollowings = async (params: GetFollowingRequest): Promise<GetFollowingResponse[]> => {
    const { data } = await axiosApi.get<GetFollowingResponse[]>(
        GET_FOLLOWING_PATH,
        { params: { ...params } })
    return data;
}

const getFollowers = async (params: GetFollowingRequest): Promise<GetFollowingResponse[]> => {
    const { data } = await axiosApi.get<GetFollowingResponse[]>(
        GET_FOLLOWERS_PATH,
        { params: { ...params } })
    return data;
}





export { getUsersWithFollowing, follow, unFollow, getFollowingCount, getFollowings, getFollowers }