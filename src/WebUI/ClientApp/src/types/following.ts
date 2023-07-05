export interface GetUsersWithFollowingResponse {
    id: string;
    userName: string;
    isFollowing: boolean;
}

export interface GetUsersWithFollowingRequest {
    searchTerm: string;
    count: number;
}

export interface FollowUserRequest {
    userId: string;
}

export interface UnFollowUserRequest {
    userId: string;
}

export interface GetFollowingCountResponse {
    followerCount: string;
    followingCount: string;
}

export interface GetFollowingCountRequest {
    userId: string;
}

export interface GetFollowingRequest {
    userId: string;
}

export interface GetFollowingResponse {
    id: string;
    userName: string;
    isFollowing: boolean;
}