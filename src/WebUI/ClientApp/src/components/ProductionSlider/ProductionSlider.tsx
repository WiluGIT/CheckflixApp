import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import useAxiosApi from "@/hooks/useAxiosApi";
import { useGetProductionsQuery } from "@/api/queries/production.query";
import ProductionCard from "../ProductionCard/ProductionCard";
import { HTMLProps } from "react";
import { MdArrowBackIos, MdArrowForwardIos } from 'react-icons/md'

const SliderNextArrow = (props: HTMLProps<HTMLElement>) => {
    const { className, style, onClick } = props;
    return (
        <div
            className={`absolute top-0 right-0 pl-1 pr-2 btn z-50 h-[98%] rounded-l-none rounded-r-sm no-animation opacity-70 hover:opacity-90 text-white ${className?.includes("slick-disabled") && 'slick-disabled '}`}
            //style={{ ...style }}
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
            //style={{ ...style }}
            onClick={onClick}
        >
            <MdArrowBackIos className="h-4 w-4" />
        </div>
    );
}

const ProductionSlider = () => {
    const axiosApi = useAxiosApi();
    const { data, isSuccess } = useGetProductionsQuery({
        page: 1,
        size: 21,
    });

    const settings = {
        className: "slider variable-width",
        infinite: false,
        slidesToShow: 6,
        slidesToScroll: 5,
        variableWidth: true,
        initialSlide: 0,
        nextArrow: <SliderNextArrow />,
        prevArrow: <SliderPrevArrow />,
        responsive: [
            {
                breakpoint: 1550,
                settings: {
                    slidesToShow: 5,
                    slidesToScroll: 5,
                }
            },
            {
                breakpoint: 1260,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 3,
                }
            },
            {
                breakpoint: 800,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 2
                }
            },
            {
                breakpoint: 520,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }
        ]
    };

    return (
        <Slider {...settings}>
            {isSuccess && data.items.map((production, idx) => (
                <div className="pr-5" key={idx}>
                    <ProductionCard production={production} />
                </div>
            ))}
        </Slider>
    );
}

export default ProductionSlider;


