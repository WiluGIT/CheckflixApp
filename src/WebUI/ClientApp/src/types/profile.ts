export interface GetProfileQueryRequest {
    userId?: string;
    accessToken?: string;
}

export interface GetProfileQueryResponse {
    id: string;
    userName: string;
    email: string;
    imageUrl: string;
    roles: string[];
}