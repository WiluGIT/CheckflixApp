import { blankCard } from "@/assets";

const ProductionCardSkeleton = () => {
    return (
        <div className="w-[200px] before:content-[''] before:h-[0px] block pb-[50px]">
            <div className="overflow-hidden inset-0 flex justify-center items-center w-full h-full bg-contain bg-no-repeat">
                <img src={blankCard} />
            </div>
        </div>
    );
}

export default ProductionCardSkeleton;