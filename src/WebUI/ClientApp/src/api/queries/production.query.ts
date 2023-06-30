import { ServerError } from "@/types/api";
import { GetProductionsRequest, GetProductionsResponse, Production } from "@/types/production";
import { UseInfiniteQueryOptions, UseQueryOptions, useInfiniteQuery, useQuery } from "@tanstack/react-query";
import axios, { AxiosInstance } from "axios";
import { getProductions } from "../services/production.service";
import { PaginationResponse } from "@/types/requests";
import { productions } from "@/mock/productionMock";

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

            // for (const movie of productionArray) {
            //     const response = await fetch(`https://api.themoviedb.org/3/movie/${movie.tmdbId}?api_key=61a4454e6812a635ebe4b24f2af2c479`, {
            //         method: "GET",
            //         mode: "cors",
            //         cache: "no-cache",
            //         headers: {
            //             "Content-Type": "application/json",
            //         },
            //         redirect: "follow",
            //         referrerPolicy: "no-referrer",
            //     });

            //     const data = await response.json();
            //     movie.poster = data['poster_path'];
            // }

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

export const useGetProductionsInfiniteQuery = (
    { pageParam = 0, size = 40 } = {},
    queryOptions: UseInfiniteQueryOptions<PaginationResponse<GetProductionsResponse>> = {}
) => {
    const query = useInfiniteQuery({
        queryKey: ['productions', 'infinite'],
        getNextPageParam: (prevData: PaginationResponse<GetProductionsResponse>) => {
            return prevData.hasNextPage ? prevData.pageNumber + 1 : undefined;
        },
        queryFn: async ({ pageParam = 1 }) => {
            return await getProductions({ pageNumber: pageParam, pageSize: size })
        },
        refetchOnMount: true,
        keepPreviousData: false,
        ...queryOptions,
    });

    return {
        ...query,
    };
};


export const useFakeSearchProductionsQuery = (
    { page = 0, size = 5, searchTerm = '' } = {},
    queryOptions: UseQueryOptions<Production[]> = {},
    axiosInstance?: AxiosInstance,
) => {
    const query = useQuery({
        queryKey: ['search', searchTerm],
        queryFn: async () => {
            await new Promise((resolve, reject) => {
                return setTimeout(resolve, 1000)
            })

            const response = productions.filter(x => x.title.toLocaleLowerCase().includes(searchTerm.toLocaleLowerCase())).slice(0, size);
            console.log("Fetched response: ", response);
            return response
        },
        keepPreviousData: true,
        ...queryOptions,
        enabled: searchTerm.length > 1
    });

    return {
        ...query,
    };
};