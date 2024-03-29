//import { useAuthContext } from "@/hooks/useAuthContext";
import HeroSlider from "@/components/HeroSlider/HeroSlider";
import ProductionList from "@/components/ProductionList/ProductionList";
import ProductionSlider from "@/components/ProductionSlider/ProductionSlider";
import { ScrollToTop } from "@/components/ScrollToTop/ScrollToTop ";
import SectionHeading from "@/components/SectionHeading/SectionHeading";
import AuthContext from "@/context/AuthContextProvider";
import useAxiosApi from "@/hooks/useAxiosApi";
import useRefreshToken from "@/hooks/useRefreshToken";
import { useContext, useState } from "react";

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
            <HeroSlider />
            <SectionHeading text={"New Trending Movies"} />
            <ProductionSlider />
            <SectionHeading text={"All Movies"} />
            <ProductionList filters={{ pageNumber: 1, pageSize: 40 }} />
            <ScrollToTop />
        </div>
    );
};

export default Home;