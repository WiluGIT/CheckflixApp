//import { useAuthContext } from "@/hooks/useAuthContext";
import AuthContext from "@/context/AuthContextProvider";
import useRefreshToken from "@/hooks/useRefreshToken";
import { api } from "@/lib/api";
import axios from "axios";
import { useContext, useEffect, useState } from "react";

type User = {
    id: string;
    email: string;
    userName: string;
}
const Home = () => {
    const [users, setUsers] = useState<User[]>();
    const refresh = useRefreshToken();
    const { authState } = useContext(AuthContext);

    // useEffect(() => {
    //     let isMounted = true;
    //     const controller = new AbortController();

    //     const getUsers = async () => {
    //         try {
    //             const { data } = await api('/Users', {
    //                 signal: controller.signal
    //             });
    //             console.log(data);
    //             isMounted && setUsers(data);
    //         } catch (err) {
    //             console.error(err);
    //         }
    //     }

    //     getUsers();
    //     console.log("User DAta: ", authState);
    //     return () => {
    //         isMounted = false;
    //         controller.abort();
    //     }
    // }, [])

    const test = () => {
        console.log("AuthencitacteD: ", authState);
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
            <button onClick={() => test()}>Refresh</button>
            <div>User Data: {JSON.stringify(authState.isLoggedIn)}</div>
        </div>
    );
};

export default Home;