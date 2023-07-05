type FollowingCardPropTypes = {
    text: string;
}

const FollowingCard: React.FC<FollowingCardPropTypes> = ({ text }) => {
    return (
        <div className="transition-all duration-300 ease-linear card bg-base-100 shadow-xl mb-3 cursor-pointer hover:bg-primary">
            <div className="card-body justify-center items-center py-2 px-7">
                {text}
            </div>
        </div>
    );
}

export default FollowingCard;