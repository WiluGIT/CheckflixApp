import Modal, { ModalPropsType } from "./Modal";
import { GetFollowingResponse } from "@/types/following";
import { useUnFollowMutation } from "@/api/queries/followings.query";
import { toast } from "react-toastify";
import { ServerError } from "@/types/api";
import { formatServerError } from "@/lib/helpers";

type FollowingModalPropsType = ModalPropsType & {
    data: GetFollowingResponse[] | undefined,
    isFollowing?: boolean,
    refetch?(): void
}

const FollowingModal: React.FC<FollowingModalPropsType> = ({ data, isFollowing = false, open, enableClickOutside, onClose, refetch }) => {
    const {
        isLoading: isLoadingUnFollow,
        mutateAsync: mutateUnFollowAsync,
    } = useUnFollowMutation({
        onSuccess: (response: string) => {
            if (refetch) {
                refetch();
            }
            toast.success(response, { theme: 'colored' });
        },
        onError: (error: ServerError) => {
            toast.error(formatServerError(error), { theme: 'colored' });
        }
    });

    const handleUnFollow = async (userId: string) => {
        await mutateUnFollowAsync({ userId: userId });
    };
    return (
        <Modal open={open} onClose={onClose} enableClickOutside={enableClickOutside}>
            <h3 className="text-lg text-white font-bold">{isFollowing ? "Followings" : "Followers"}</h3>
            <div className="overflow-x-auto ">
                <table className="table">
                    <thead>
                        <tr className="text-white border-white/80">
                            <th></th>
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
                                    {el.isFollowing && <button onClick={() => handleUnFollow(el.id)} type="button" className="btn btn-outline bg-[#a6adba] text-[#1d232f]">
                                        UnFollow
                                    </button>}
                                </th>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
            <div className="modal-action">
                <button className="btn btn-primary" onClick={onClose}>
                    close
                </button>
            </div>
        </Modal>
    );
}

export default FollowingModal;