import { api } from "@/lib/api";

const useRefreshToken = () => {
    //const { user, setUser } = useAuthContext();

    const refresh = async () => {
        const { data } = await api.get('/Tokens/refresh', {
            withCredentials: true
        });

        // console.log("PREV: ", JSON.stringify(user));
        // console.log("DATA: ", data);
        // debugger;
        // setUser({
        //     id: '3213',
        //     name: 'dsadsa',
        //     email: 'dsadsa',
        //     authToken: 'dsads',
        // });

        return data?.accessToken;
    }

    return refresh;
}

export default useRefreshToken;