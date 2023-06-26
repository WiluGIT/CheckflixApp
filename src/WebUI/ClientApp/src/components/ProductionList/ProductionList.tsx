import { useGetProductionsInfiniteQuery } from "@/api/queries/production.query";
import useAxiosApi from "@/hooks/useAxiosApi";
import InfiniteScroll from "react-infinite-scroll-component";

const ProductionList = () => {
    const axiosApi = useAxiosApi();
    const {
        fetchNextPage,
        isSuccess,
        hasNextPage,
        isFetchingNextPage,
        data,
        status,
        error } = useGetProductionsInfiniteQuery({ pageParam: 1, size: 10 });


    const productions = data?.pages.reduce((prod: any, page: any) => {
        return [...prod, ...page.items]
    }, [])


    return (
        <div className="">
            <div className="prose prose-slate lg:prose-lg text-white mb-5">
                <h3 className="text-white ">All Movies</h3>
            </div>
            <div>{productions?.length}</div>


            <InfiniteScroll
                dataLength={productions ? productions.length : 0}
                next={() => fetchNextPage()}
                hasMore={hasNextPage || false}
                loader={<div>testsejtidjfiosdjfiosdjoifdsj</div>}
            >
                <div className="grid gap-6 grid-cols-1 xl:grid-cols-5 lg:grid-cols-4 md:grid-cols-3 xs:grid-cols-2">
                    {productions &&
                        productions.map((production, idx) => (
                            <div className="card mx-auto w-[200px]" key={idx}>
                                <span className="absolute top-0 text-white ">{idx}</span>
                                <figure className="">
                                    <img src="https://image.tmdb.org/t/p/w500/vgpXmVaVyUL7GGiDeiK1mKEKzcX.jpg" className="rounded-lg shadow-lg" />
                                </figure>
                            </div>
                        ))

                    }
                </div>
            </InfiniteScroll>
        </div >
    );
}

export default ProductionList;