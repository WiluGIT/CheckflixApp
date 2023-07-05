import { useGetFollowersQuery, useGetFollowingCountQuery, useGetFollowingsQuery } from "@/api/queries/followings.query";
import { useGetProfileQuery } from "@/api/queries/personal.query";
import { useGetUserCollectionsQuery } from "@/api/queries/userProductions.query";
import FollowingCard from "@/components/FollowingCard/FollowingCard";
import FollowingModal from "@/components/Modal/FollowingModal";
import Modal from "@/components/Modal/Modal";
import ProductionSlider from "@/components/ProductionSlider/ProductionSlider";
import SectionHeading from "@/components/SectionHeading/SectionHeading";
import { useState } from "react";
import { useParams } from "react-router-dom";

const Profile = () => {
    const { userId } = useParams();
    const [openFollowingModal, setOpenFollowingModal] = useState(false);
    const [openFollowersModal, setOpenFollowersModal] = useState(false);
    const { data: followingCountData, refetch: refetchFollowingCount } = useGetFollowingCountQuery({ userId: userId || '' });
    const { data: collectionsData } = useGetUserCollectionsQuery({ userId: userId || '' });
    const { data: profileData } = useGetProfileQuery({ userId: userId || '' });
    const { data: followingData, refetch: refetchFollowing } = useGetFollowingsQuery({ userId: userId || '' }, openFollowingModal);
    const { data: followersData } = useGetFollowersQuery({ userId: userId || '' }, openFollowersModal);
    const handleFollowingToggle = () => { setOpenFollowingModal((prev) => !prev); }
    const handleFollowersToggle = () => { setOpenFollowersModal((prev) => !prev); }

    const refetchData = () => {
        refetchFollowingCount();
        refetchFollowing();
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
                    <h2 className="mb-3 text-white">{profileData?.userName}</h2>
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
            <ProductionSlider />
            <SectionHeading text={"Watched"} />
            <ProductionSlider />
            <SectionHeading text={"To Watch"} />
            <ProductionSlider />
            <FollowingModal open={openFollowingModal} onClose={handleFollowingToggle} enableClickOutside={true} data={followingData} isFollowing={true} refetch={refetchData} />
            <FollowingModal open={openFollowersModal} onClose={handleFollowersToggle} enableClickOutside={true} data={followersData} />
        </div>
    );
}

export default Profile;