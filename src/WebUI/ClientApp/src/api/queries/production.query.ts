import { ServerError } from "@/types/api";
import { GetProductionsRequest, GetProductionsResponse } from "@/types/production";
import { UseQueryOptions, useQuery } from "@tanstack/react-query";
import { AxiosInstance } from "axios";
import { getProductions } from "../services/production.service";
import { PaginationResponse } from "@/types/requests";

// export const useGetProductionsQuery = (
//     config: UseQueryOptions<GetProductionsResponse, ServerError, GetProductionsRequest>,
//     axiosInstance?: AxiosInstance,
// ) => {
//     return useQuery(
//         ['productions'],
//         async (params: GetProductionsRequest) => {
//             return await getProductions(params, axiosInstance);
//         },
//         {
//             ...config,
//         }
//     );
// };

// const usersKeys = createQueryKeys('usersService', {
//     users: (params: { page?: number; size?: number }) => [params],
//     user: (params: { login?: string }) => [params],
//     userForm: null,
//   });

export const useGetProductionsQuery = (
    { page = 0, size = 10 } = {},
    queryOptions: UseQueryOptions<PaginationResponse<GetProductionsResponse>> = {},
    axiosInstance?: AxiosInstance,
) => {
    const query = useQuery({
        queryKey: ['users', { page, size }],
        queryFn: async () => {
            // const response = await Axios.get(USERS_BASE_URL, {
            //     params: { page, size, sort: 'id,desc' },
            // });
            // return zUserList().parse({
            //     users: response.data,
            //     totalItems: response.headers?.['x-total-count'],
            // });
            const response = await getProductions({ pageNumber: page, pageSize: size, orderBy: 'releaseDate desc', keyword: 'nolan' }, axiosInstance);
            return response;
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