import Slider, { Settings } from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import ProductionCard from "../ProductionCard/ProductionCard";
import { HTMLProps } from "react";
import { MdArrowBackIos, MdArrowForwardIos } from 'react-icons/md'
import { BasicProduction } from "@/types/production";

type ProductionSliderPropTypes = {
    data: BasicProduction[];
    settings: Settings;
}

const SliderNextArrow = (props: HTMLProps<HTMLElement>) => {
    const { className, style, onClick } = props;
    return (
        <div
            className={`absolute top-0 right-0 pl-1 pr-2 btn z-50 h-[98%] rounded-l-none rounded-r-sm no-animation opacity-70 hover:opacity-90 text-white ${className?.includes("slick-disabled") && 'slick-disabled '}`}
            onClick={onClick}
        >
            <MdArrowForwardIos className="h-4 w-4 opacity-100" />
        </div>
    );
}

const SliderPrevArrow = (props: HTMLProps<HTMLElement>) => {
    const { className, style, onClick } = props;
    return (
        <div
            className={`absolute top-0 left-0 pr-1 pl-2 btn z-50 h-[98%] rounded-r-none rounded-l-sm no-animation opacity-70 hover:opacity-90 text-white ${className?.includes("slick-disabled") && 'slick-disabled '}`}
            onClick={onClick}
        >
            <MdArrowBackIos className="h-4 w-4" />
        </div>
    );
}

const ProductionSlider: React.FC<ProductionSliderPropTypes> = ({ data, settings }) => {
    const settingsBase: Settings = {
        nextArrow: <SliderNextArrow />,
        prevArrow: <SliderPrevArrow />,
    };
    return (
        <Slider {...{ ...settingsBase, ...settings }}>
            {data?.map((production, idx) => (
                <div className="pr-5" key={idx}>
                    <ProductionCard production={production} />
                </div>
            ))}
        </Slider>
    );
}

export default ProductionSlider;


