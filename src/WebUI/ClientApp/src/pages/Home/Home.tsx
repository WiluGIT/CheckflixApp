//import { useAuthContext } from "@/hooks/useAuthContext";
import AuthContext from "@/context/AuthContextProvider";
import useAxiosApi from "@/hooks/useAxiosApi";
import useRefreshToken from "@/hooks/useRefreshToken";
import { axiosApi as api } from "@/lib/api";
import { useContext, useEffect, useState } from "react";

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

    useEffect(() => {
        console.log("HOMMMEee")
        let isMounted = true;
        const controller = new AbortController();

        const getUsers = async () => {
            try {
                const { data } = await axiosApi.get('/Users', {
                    signal: controller.signal
                });
                console.log(data);
                isMounted && setUsers(data);
            } catch (err) {
                console.error(err);
            }
        }

        getUsers();
        //console.log("User DAta: ", authState);
        return () => {
            isMounted = false;
            controller.abort();
        }
    }, [])

    const test = async () => {
        console.log("AuthencitacteD: ", authState);
        try {
            console.log("fetching")

            const response = await axiosApi.get('/Users');
            console.log(response)
            // Handle the response data
            setUsers(response.data);
        } catch (error) {
            // Handle errors
        }
    }

    return (
        <div className="home">
            Home page
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
            <div>User Data: {JSON.stringify(authState.isAuthenticated)}</div>
        </div>
    );
};

export default Home;