import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import useAxiosApi from "@/hooks/useAxiosApi";
import { useGetProductionsQuery } from "@/api/queries/production.query";

const ProductionSlider = () => {
    const axiosApi = useAxiosApi();

    // const {
    //     isLoading,
    //     mutateAsync,
    // } = useLoginMutation({
    //     onSuccess: (response: LoginResponse) => {
    //         toast.success("Sucessfully logged in", { theme: 'colored' });
    //         globalLogInDispatch(response);
    //     },
    //     onError: (error: ServerError) => {
    //         toast.error(formatServerError(error), { theme: 'colored' });
    //     }
    // }, axiosApi);

    const users = useGetProductionsQuery({
        page: 1,
        size: 10,
    });

    const settings = {
        className: "center",
        infinite: true,
        centerPadding: "60px",
        slidesToShow: 5,
        swipeToSlide: true,
        afterChange: function (index) {
            console.log(
                `Slider Changed to: ${index + 1}, background: #222; color: #bada55`
            );
        }
    };
    return (
        <div className="m-auto w-[50%]">
            DATA = {JSON.stringify(users?.data)}
            <Slider {...settings}>
                {[1, 2, 3, 4, 6, 7, 8].map((el, idx) => (
                    <div className="pl-5" key={idx}>
                        <img src="https://www.imdb.com/title/tt11737520/mediaviewer" />
                    </div>
                ))}
            </Slider>
        </div>
    );
}

export default ProductionSlider;