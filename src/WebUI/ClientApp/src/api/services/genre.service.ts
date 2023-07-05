import { GET_GENRES_PATH, GET_GENRES_PRODUCTIONS_PATH } from "@/paths/api/genrePaths";
import { Genre, GetGenresProductionsRequest, GetGenresProductionsResponse } from "@/types/genre";
import { axiosApi } from "@/lib/api";
import { PaginationResponse } from "@/types/requests";

const getGenres = async (): Promise<Genre[]> => {
    const { data } = await axiosApi.get<Genre[]>(GET_GENRES_PATH);
    return data;
};

const getGenresProductions = async (params: GetGenresProductionsRequest): Promise<PaginationResponse<GetGenresProductionsResponse>> => {
    const { data } = await axiosApi.get<PaginationResponse<GetGenresProductionsResponse>>(
        GET_GENRES_PRODUCTIONS_PATH,
        { params: { ...params } });
    return data;
};

export { getGenres, getGenresProductions };