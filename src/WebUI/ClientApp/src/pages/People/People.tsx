import { useFollowMutation, useGetUsersWithFollowingQuery, useUnFollowMutation } from "@/api/queries/followings.query";
import Loader from "@/components/Loader/Loader";
import ProductionList from "@/components/ProductionList/ProductionList";
import { ScrollToTop } from "@/components/ScrollToTop/ScrollToTop ";
import SectionHeading from "@/components/SectionHeading/SectionHeading";
import useAxiosApi from "@/hooks/useAxiosApi";
import useDebounce from "@/hooks/useDebounce";
import { formatServerError } from "@/lib/helpers";
import { ServerError } from "@/types/api";
import { useEffect, useState } from "react";
import { useSearchParams } from "react-router-dom";
import { toast } from "react-toastify";

const People = () => {
    const axiosApi = useAxiosApi();
    const [searchParams, setSearchParams] = useSearchParams();
    const [searchTerm, setSearchTerm] = useState<string>('');
    const debouncedSearchTerm = useDebounce(searchTerm, 500);
    const { data, isSuccess, isLoading, refetch } = useGetUsersWithFollowingQuery({ searchTerm: debouncedSearchTerm, count: 200 });
    const {
        isLoading: isLoadingFollow,
        mutateAsync: mutateFollowAsync,
    } = useFollowMutation({
        onSuccess: (response: string) => {
            refetch();
            toast.success(response, { theme: 'colored' });
        },
        onError: (error: ServerError) => {
            toast.error(formatServerError(error), { theme: 'colored' });
        }
    });
    const {
        isLoading: isLoadingUnFollow,
        mutateAsync: mutateUnFollowAsync,
    } = useUnFollowMutation({
        onSuccess: (response: string) => {
            refetch();
            toast.success(response, { theme: 'colored' });
        },
        onError: (error: ServerError) => {
            toast.error(formatServerError(error), { theme: 'colored' });
        }
    });

    useEffect(() => {
        setSearchTerm(searchParams.get("query") || '');
    }, [searchParams])

    const handleFollow = async (userId: string) => {
        await mutateFollowAsync({ userId: userId });
    };

    const handleUnFollow = async (userId: string) => {
        await mutateUnFollowAsync({ userId: userId });
    };

    return (
        <div className="flex flex-col ">
            <form className={`bg-people-search-gradient text-white border border-solid border-zinc-300/50 rounded-lg max-h-11 w-[300px] mt-9 relative  pt-[1px]`}>
                <div className="flex w-full h-[42px] items-center">
                    <div className='grow'>
                        <input
                            type='text'
                            value={searchTerm}
                            onChange={(e) => setSearchTerm(e.target.value)}
                            className='h-full w-full overflow-hidden border-none outline-none bg-[initial] pl-4' placeholder='Search People'
                        />
                    </div>
                </div>
            </form >
            <SectionHeading text={`People`} />
            {isLoading && <Loader classes="w-14" loaderType={"loading-infinity"} />}
            <div className="overflow-x-auto ">
                <table className="table">
                    <thead>
                        <tr className="text-white border-white/80">
                            <th>User</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody className="text-white">
                        {data?.map((el, idx) => (
                            <tr key={el.id} className="border-white/30">
                                <td>
                                    <div className="flex items-center space-x-3">
                                        <div className="avatar">
                                            <div className="mask mask-squircle w-12 h-12">
                                                <img src="http://daisyui.com/tailwind-css-component-profile-3@56w.png" alt="Avatar Tailwind CSS Component" />
                                            </div>
                                        </div>
                                        <div>
                                            <div className="font-bold">{el.userName}</div>
                                        </div>
                                    </div>
                                </td>
                                <th className="text-end">
                                    {el.isFollowing ? (
                                        <button onClick={() => handleUnFollow(el.id)} type="button" className="btn btn-outline bg-[#a6adba] text-[#1d232f]">
                                            UnFollow
                                        </button>
                                    ) : (
                                        <button onClick={() => handleFollow(el.id)} className="btn btn-outline">
                                            Follow
                                        </button>
                                    )}

                                </th>
                            </tr>
                        ))}
                    </tbody>
                    <tfoot>
                        <tr>
                            <th>User</th>
                            <th></th>
                        </tr>
                    </tfoot>
                </table>
            </div>
            <ScrollToTop />
        </div>
    );
}

export default People;