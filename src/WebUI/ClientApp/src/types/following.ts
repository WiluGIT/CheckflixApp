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