import { GetUserCollectionsRequest, GetUserCollectionsResponse } from "@/types/userCollections";
import { UseQueryOptions, useQuery } from "@tanstack/react-query";
import { getUserCollections } from "../services/userProductions.service";

export const useGetUserCollectionsQuery = (
    params: GetUserCollectionsRequest,
    queryOptions: UseQueryOptions<GetUserCollectionsResponse> = {},
) => {
    const query = useQuery({
        queryKey: ['userCollections', params],
        queryFn: async () => {
            const response = await getUserCollections(params);
            return response
        },
        keepPreviousData: true,
        ...queryOptions
    });

    return {
        ...query,
    };
};