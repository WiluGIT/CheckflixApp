import { axiosApi } from "@/lib/api";
import { FOLLOW_USER_PATH, GET_USERS_WITH_FOLLOWING_PATH, UNFOLLOW_USER_PATH } from "@/paths/api/followingPaths";
import { GetUsersWithFollowingRequest, GetUsersWithFollowingResponse } from "@/types/following";

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


export { getUsersWithFollowing, follow, unFollow }