import useAxiosApi from "@/hooks/useAxiosApi";
import InfiniteScroll from "react-infinite-scroll-component";
import ProductionCard from "../ProductionCard/ProductionCard";
import { GetProductionsResponse, Production } from "@/types/production";
import { PaginationFilter, PaginationResponse } from "@/types/requests";
import Loader from "@/components/Loader/Loader";
import { useGetGenresProductionsInfiniteQuery } from "@/api/queries/genre.query";
import { GenreFilter } from "@/types/genre";

type ProductionListPropsType = {
    filters: GenreFilter;
}

const GenreProductionList: React.FC<ProductionListPropsType> = ({ filters }) => {
    const axiosApi = useAxiosApi();
    const {
        fetchNextPage,
        hasNextPage,
        data,
        status } = useGetGenresProductionsInfiniteQuery(filters);


    const productions = data?.pages.reduce((prod: Production[], page: PaginationResponse<GetProductionsResponse>) => {
        return [...prod, ...page.items]
    }, [])


    return (
        <InfiniteScroll
            dataLength={productions ? productions.length : 0}
            next={() => fetchNextPage()}
            hasMore={hasNextPage || false}
            loader={<Loader classes="w-14" loaderType={"loading-infinity"} />}
        >
            <div className="grid grid-cols-[repeat(auto-fill,minmax(200px,1fr))] gap-3">
                {productions &&
                    productions.map((production, idx) => (
                        <ProductionCard production={production} key={idx} />
                    ))
                }
            </div>
        </InfiniteScroll>
    );
}

export default GenreProductionList;