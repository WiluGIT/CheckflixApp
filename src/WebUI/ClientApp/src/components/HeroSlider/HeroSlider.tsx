import Slider from "react-slick";
import { MdArrowBackIos, MdArrowForwardIos } from 'react-icons/md'
import { HTMLProps } from "react";
import { servicesHero, servicesHeroBg, tvHero } from "@/assets";

const heroSliderData = [
    {
        image: servicesHero,
        text: 'The Best Movie & TV Tracker',
        subtext: 'Track all the shows you watch & add movies you want to see ',
        buttonText: 'Profile',
        bgClass: 'bg-gradient-to-r from-violet-950 to-fuchsia-800',
        imgClass: 'object-contain',
        buttonLink: '',
    },
    {
        image: servicesHeroBg,
        text: 'All your streaming services in one place.',
        subtext: 'Browse, search, and mange TV & Movies',
        buttonText: 'Browse',
        bgClass: 'bg-gradient-to-r from-gray-900 to-gray-700',
        imgClass: 'object-cover',
        buttonLink: '',
    }
]

const SliderNextArrow = (props: HTMLProps<HTMLElement>) => {
    const { className, style, onClick } = props;
    return (
        <div
            className={` text-white absolute right-1 top-0 cursor-pointer`}
            onClick={onClick}
        >
            <MdArrowForwardIos className="h-7 w-7" />
        </div>
    );
}

const SliderPrevArrow = (props: HTMLProps<HTMLElement>) => {
    const { className, style, onClick } = props;
    return (
        <div
            className={`text-white absolute right-10 top-0 cursor-pointer`}
            onClick={onClick}
        >
            <MdArrowBackIos className="h-7 w-7" />
        </div>
    );
}

const HeroSlider = () => {
    const settings = {
        infinite: true,
        initialSlide: 0,
        nextArrow: <SliderNextArrow />,
        prevArrow: <SliderPrevArrow />,
        dots: true,
        autoplay: true,
        autoplaySpeed: 5000,
    }

    return (
        <Slider {...settings} className="pt-10 mb-10 w-[70%] mx-auto">
            {
                heroSliderData.map((slide, idx) => (
                    <div className={`flex flex-col h-[480px] w-full relative ${slide.bgClass} bg-gradient-[#fffff] rounded-lg shadow-lg`} key={idx}>
                        <img src={slide.image} className={`rounded-lg shadow-lg absolute w-full h-full opacity-30 ${slide.imgClass}`} />
                        <div className="max-w-md card-body absolute prose prose-slate text-white my-5 left-[5%] bottom-[5%]">
                            <h2 className="text-white mb-[0px]">{slide.text}</h2>
                            <p>{slide.subtext}</p>
                            <div className="card-actions">
                                <button className="btn btn-primary rounded-full">{slide.buttonText}</button>
                            </div>
                        </div>
                    </div>
                ))
            }
        </Slider >
    );
}

export default HeroSlider;