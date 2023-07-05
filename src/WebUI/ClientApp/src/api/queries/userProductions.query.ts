import { GetUserCollectionsRequest, GetUserCollectionsResponse } from "@/types/userCollections";
import { UseQueryOptions, useQuery } from "@tanstack/react-query";
import { getUserCollections } from "../services/userProductions.service";
import { basicProductionMock } from "@/mock/productionMock";

export const useGetUserCollectionsQuery = (
    params: GetUserCollectionsRequest,
    queryOptions: UseQueryOptions<GetUserCollectionsResponse> = {},
) => {
    const query = useQuery({
        queryKey: ['userCollections', params],
        queryFn: async () => {
            const response = await getUserCollections(params);

            while (response.favourites.length < 10) {
                response.favourites.push(basicProductionMock[0])
            }

            while (response.watched.length < 10) {
                response.watched.push(basicProductionMock[0])
            }

            while (response.toWatch.length < 10) {
                response.toWatch.push(basicProductionMock[0])
            }

            return response
        },
        keepPreviousData: true,
        ...queryOptions
    });

    return {
        ...query,
    };
};