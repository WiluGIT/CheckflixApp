import { UseMutationOptions, UseQueryOptions, useQuery, useMutation } from "@tanstack/react-query";
import { follow, getUsersWithFollowing, unFollow } from "../services/followings.service";
import { FollowUserRequest, GetUsersWithFollowingRequest, GetUsersWithFollowingResponse, UnFollowUserRequest } from "@/types/following";
import { ServerError } from "@/types/api";

export const useGetUsersWithFollowingQuery = (
    params: GetUsersWithFollowingRequest,
    queryOptions: UseQueryOptions<GetUsersWithFollowingResponse[]> = {},
) => {
    const query = useQuery({
        queryKey: ['usersWithFollowing', params],
        queryFn: async () => {
            const response = await getUsersWithFollowing(params);
            return response
        },
        keepPreviousData: true,
        ...queryOptions
    });

    return {
        ...query,
    };
};

export const useFollowMutation = (
    config: UseMutationOptions<string, ServerError, FollowUserRequest>,
) => {
    return useMutation(
        ['follow'],
        async (params: FollowUserRequest) => {
            return await follow(params.userId);
        },
        {
            ...config,
            onSuccess: (data, ...args) => {
                config?.onSuccess?.(data, ...args);
            },
        }
    );
};

export const useUnFollowMutation = (
    config: UseMutationOptions<string, ServerError, UnFollowUserRequest>,
) => {
    return useMutation(
        ['unfollow'],
        async (params: UnFollowUserRequest) => {
            return await unFollow(params.userId);
        },
        {
            ...config,
            onSuccess: (data, ...args) => {
                config?.onSuccess?.(data, ...args);
            },
        }
    );
};