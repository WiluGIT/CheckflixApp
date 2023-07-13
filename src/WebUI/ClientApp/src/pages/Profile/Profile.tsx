import { useGetFollowersQuery, useGetFollowingCountQuery, useGetFollowingsQuery } from "@/api/queries/followings.query";
import { useGetProfileQuery } from "@/api/queries/personal.query";
import { useGetUserCollectionsQuery } from "@/api/queries/userProductions.query";
import FollowingCard from "@/components/FollowingCard/FollowingCard";
import FollowingModal from "@/components/Modal/FollowingModal";
import ProductionSlider from "@/components/ProductionSlider/ProductionSlider";
import SectionHeading from "@/components/SectionHeading/SectionHeading";
import AuthContext from "@/context/AuthContextProvider";
import useAxiosApi from "@/hooks/useAxiosApi";
import { useContext, useState } from "react";
import { useParams } from "react-router-dom";
import { Settings } from "react-slick";

const Profile = () => {
    const axiosApi = useAxiosApi();
    const { userId } = useParams();
    const { authState } = useContext(AuthContext);
    const [openFollowingModal, setOpenFollowingModal] = useState(false);
    const [openFollowersModal, setOpenFollowersModal] = useState(false);
    const { data: followingCountData, refetch: refetchFollowingCount } = useGetFollowingCountQuery({ userId: userId || '' });
    const { data: collectionsData } = useGetUserCollectionsQuery({ userId: userId || '' });
    const { data: followingData, refetch: refetchFollowing } = useGetFollowingsQuery({ userId: userId || '' }, openFollowingModal);
    const { data: followersData } = useGetFollowersQuery({ userId: userId || '' }, openFollowersModal);
    const handleFollowingToggle = () => { setOpenFollowingModal((prev) => !prev); }
    const handleFollowersToggle = () => { setOpenFollowersModal((prev) => !prev); }

    const refetchData = () => {
        refetchFollowingCount();
        refetchFollowing();
    };

    const settings: Settings = {
        className: "slider variable-width",
        infinite: true,
        slidesToShow: 1,
        slidesToScroll: 2,
        variableWidth: true,
        initialSlide: 0,
    };

    return (
        <div className="flex flex-col">
            <SectionHeading text={"Profile"} />
            <div className="flex items-start gap-10">
                <div className="avatar">
                    <div className="w-[160px] mask mask-squircle">
                        <img src="https://daisyui.com/images/stock/photo-1534528741775-53994a69daeb.jpg" />
                    </div>
                </div>
                <div className="flex flex-col prose prose-slate text-white">
                    <h2 className="mb-3 text-white">{authState?.user?.userName}</h2>
                    <div onClick={() => handleFollowingToggle()}>
                        <FollowingCard text={`${followingCountData?.followingCount} Following`} />
                    </div>
                    <div onClick={() => handleFollowersToggle()}>
                        <FollowingCard text={`${followingCountData?.followerCount} Followers`} />
                    </div>
                </div>
            </div>
            <div className="divider mt-10"></div>
            <SectionHeading text={"Favourite"} />
            <ProductionSlider data={collectionsData?.favourites || []} settings={settings} />
            <SectionHeading text={"Watched"} />
            <ProductionSlider data={collectionsData?.watched || []} settings={settings} />
            <SectionHeading text={"To Watch"} />
            <ProductionSlider data={collectionsData?.toWatch || []} settings={settings} />
            <FollowingModal open={openFollowingModal} onClose={handleFollowingToggle} enableClickOutside={true} data={followingData} isFollowing={true} refetch={refetchData} />
            <FollowingModal open={openFollowersModal} onClose={handleFollowersToggle} enableClickOutside={true} data={followersData} />
        </div>
    );
}

export default Profile;