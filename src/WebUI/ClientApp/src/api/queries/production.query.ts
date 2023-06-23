import { ServerError } from "@/types/api";
import { GetProductionsRequest, GetProductionsResponse } from "@/types/production";
import { UseQueryOptions, useQuery } from "@tanstack/react-query";
import axios, { AxiosInstance } from "axios";
import { getProductions } from "../services/production.service";
import { PaginationResponse } from "@/types/requests";

export const useGetProductionsQuery = (
    { page = 0, size = 10 } = {},
    queryOptions: UseQueryOptions<PaginationResponse<GetProductionsResponse>> = {},
    axiosInstance?: AxiosInstance,
) => {
    const query = useQuery({
        queryKey: ['users', { page, size }],
        queryFn: async () => {
            const response = await getProductions({ pageNumber: page, pageSize: size, orderBy: 'releaseDate desc' }, axiosInstance);
            const productionArray: Production[] = response.items;

            for (const movie of productionArray) {
                const response = await fetch(`https://api.themoviedb.org/3/movie/${movie.tmdbId}?api_key=61a4454e6812a635ebe4b24f2af2c479`, {
                    method: "GET",
                    mode: "cors",
                    cache: "no-cache",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    redirect: "follow",
                    referrerPolicy: "no-referrer",
                });

                const data = await response.json();
                movie.poster = data['poster_path'];
            }

            return response
        },
        keepPreviousData: true,
        ...queryOptions,
    });

    const items = query.data?.items;
    const totalItems = query.data?.totalCount ?? 0;
    const totalPages = query.data?.totalPages ?? 0;
    const hasMore = query.data?.hasNextPage;
    const isLoadingPage = query.isFetching;

    return {
        items,
        totalItems,
        hasMore,
        totalPages,
        isLoadingPage,
        ...query,
    };
};


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
    poster?: string;
}
