import { UseInfiniteQueryOptions, UseQueryOptions } from "@tanstack/react-query";
import { useQuery, useInfiniteQuery } from "@tanstack/react-query";
import { getGenres, getGenresProductions } from "../services/genre.service";
import { Genre, GetGenresProductionsRequest, GetGenresProductionsResponse } from "@/types/genre";
import { PaginationResponse } from "@/types/requests";

export const useGetGenresQuery = (
    queryOptions: UseQueryOptions<Genre[]> = {},
) => {
    const query = useQuery({
        queryKey: ['genres'],
        queryFn: async () => {
            const response = await getGenres();
            return response
        },
        keepPreviousData: true,
        ...queryOptions
    });

    return {
        ...query,
    };
};

export const useGetGenresProductionsInfiniteQuery = (
    params: GetGenresProductionsRequest = { pageNumber: 1, pageSize: 10, genreIds: [] },
    queryOptions: UseInfiniteQueryOptions<PaginationResponse<GetGenresProductionsResponse>> = {}
) => {
    const query = useInfiniteQuery({
        queryKey: ['infinite', [{ ...params }]],
        getNextPageParam: (prevData: PaginationResponse<GetGenresProductionsResponse>) => {
            return prevData.hasNextPage ? prevData.pageNumber + 1 : undefined;
        },
        queryFn: async ({ pageParam = 1 }) => {
            return await getGenresProductions({ ...params, pageNumber: pageParam })
        },
        refetchOnMount: true,
        keepPreviousData: false,
        ...queryOptions,
    });

    return {
        ...query,
    };
};