export interface GetProfileQueryRequest {
    userId: string;
}

export interface GetProfileQueryResponse {
    id: string;
    userName: string;
    email: string;
    imageUrl: string;
}