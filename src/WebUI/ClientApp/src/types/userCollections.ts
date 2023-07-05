import { BasicProduction } from "./production";

export interface GetUserCollectionsRequest {
    userId: string;
}

export interface GetUserCollectionsResponse {
    favourites: BasicProduction[];
    toWatch: BasicProduction[];
    watched: BasicProduction[];
}