import { PaginationFilter } from "./requests";

export interface Genre {
    id: number;
    name: string;
}
export interface GenreFilter extends PaginationFilter {
    genreIds: number[];
}

export interface GetGenresProductionsRequest extends PaginationFilter {
    genreIds: number[];
}

export interface GetGenresProductionsResponse {
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