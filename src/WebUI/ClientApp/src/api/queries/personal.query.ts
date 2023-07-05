import { GetProfileQueryRequest, GetProfileQueryResponse } from "@/types/profile";
import { UseQueryOptions, useQuery } from "@tanstack/react-query";
import { getProfile } from "../services/personal.service";

export const useGetProfileQuery = (
    params: GetProfileQueryRequest,
    queryOptions: UseQueryOptions<GetProfileQueryResponse> = {},
) => {
    const query = useQuery({
        queryKey: ['profile', params],
        queryFn: async () => {
            const response = await getProfile(params);
            return response
        },
        keepPreviousData: true,
        ...queryOptions
    });

    return {
        ...query,
    };
};