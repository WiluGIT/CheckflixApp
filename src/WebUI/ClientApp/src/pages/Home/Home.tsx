//import { useAuthContext } from "@/hooks/useAuthContext";
import { useGetProductionsQuery } from "@/api/queries/production.query";
import HeroSlider from "@/components/HeroSlider/HeroSlider";
import ProductionList from "@/components/ProductionList/ProductionList";
import ProductionSlider from "@/components/ProductionSlider/ProductionSlider";
import { ScrollToTop } from "@/components/ScrollToTop/ScrollToTop ";
import SectionHeading from "@/components/SectionHeading/SectionHeading";
import AuthContext from "@/context/AuthContextProvider";
import useAxiosApi from "@/hooks/useAxiosApi";
import useRefreshToken from "@/hooks/useRefreshToken";
import { useContext, useState } from "react";
import { Settings } from "react-slick";

type User = {
    id: string;
    email: string;
    userName: string;
}
const Home = () => {
    const { authState } = useContext(AuthContext);
    const [users, setUsers] = useState<User[]>();
    const refresh = useRefreshToken();
    const axiosApi = useAxiosApi();
    const { data, isSuccess } = useGetProductionsQuery({
        pageNumber: 1,
        pageSize: 21,
        orderBy: 'releaseDate desc'
    });
    const settings: Settings = {
        className: "slider variable-width",
        infinite: false,
        slidesToShow: 1,
        slidesToScroll: 2,
        variableWidth: true,
        initialSlide: 0,
        responsive: [
            {
                breakpoint: 1550,
                settings: {
                    slidesToShow: 5,
                    slidesToScroll: 5,
                }
            },
            {
                breakpoint: 1285,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 3,
                }
            },
            {
                breakpoint: 850,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 2
                }
            },
            {
                breakpoint: 630,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }
        ]
    };
    const test = async () => {
        try {
            const response = await axiosApi.get('/Users');
            // Handle the response data
            setUsers(response.data);
        } catch (error) {
            // Handle errors
        }
    }

    return (
        <div className="home">
            {/* Home page
            <div>
                {users?.map((el) => (
                    <div key={el.id} className="text-white">
                        <span>{el.id}---------</span>
                        <span>{el.email}</span>
                        <br />
                    </div>
                ))}
            </div>
            <button onClick={() => test()}>Authstate</button>
            <button onClick={() => refresh()}>Refresh</button>
            <div>User Data: {JSON.stringify(authState.isAuthenticated)}</div> */}
            {/* {JSON.stringify(data)} */}
            <HeroSlider />
            <SectionHeading text={"New Trending Movies"} />
            <ProductionSlider data={data?.items || []} settings={settings} />
            <SectionHeading text={"All Movies"} />
            <ProductionList filters={{ pageNumber: 1, pageSize: 40 }} />
            <ScrollToTop />
        </div>
    );
};

export default Home;