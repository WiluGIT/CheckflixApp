import { PaginationFilter } from "./requests";

export interface GetProductionsRequest extends PaginationFilter {
}

export interface GetProductionsResponse {
    productionId: number;
    tmdbId: string;
    imdbId: string;
    title: string;
    overview: string;
    director: string;
    keywords: string;
    releaseDate: Date;
    genres: string[];
}

export interface Production {
    productionId: number;
    tmdbId: string;
    imdbId: string;
    title: string;
    overview: string;
    director: string;
    keywords: string;
    releaseDate: Date;
    genres: string[];
}

export interface BasicProduction {
    productionId: number;
    tmdbId: string;
    imdbId: string;
    title: string;
    releaseDate: Date;
}

