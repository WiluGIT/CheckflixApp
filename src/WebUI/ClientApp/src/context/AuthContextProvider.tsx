import {
    createContext,
    useReducer,
    useCallback,
    useEffect,
} from "react";
import { useNavigate } from "react-router-dom";

import { AuthActionEnum } from "../store/auth/authActions";
import authReducer from "../store/auth/authReducer";
import { AuthContextType, AuthProviderProps, AuthState, UserData } from "@/types/auth";

export const defaultAuthState: AuthState = {
    isLoggedIn: false,
};

export const AuthContext = createContext<AuthContextType>({
    authState: defaultAuthState,
    globalLogInDispatch: () => { },
    globalLogOutDispatch: () => { },
});

export const AuthContextProvider = (props: AuthProviderProps) => {
    const { children } = props;

    const [authState, authDispatch] = useReducer(authReducer, defaultAuthState);
    const navigate = useNavigate();

    // browser refresh user init
    useEffect(() => {
        debugger;
        const user = localStorage.getItem("user");
        if (user) {
            const userData: UserData = JSON.parse(user);
            authDispatch({ type: AuthActionEnum.LOG_IN, payload: userData });
        }
    }, []);

    const globalLogInDispatch = useCallback((userData: UserData) => {
        const { authToken, email, name, userId } = userData;
        authDispatch({
            type: AuthActionEnum.LOG_IN,
            payload: {
                authToken,
                userId,
                name,
                email,
            },
        });
        navigate("/");
    },
        [navigate]
    );

    const globalLogOutDispatch = useCallback(() => {
        authDispatch({ type: AuthActionEnum.LOG_OUT, payload: null });
        navigate("/login");
    }, [navigate]);

    const ctx = {
        authState,
        globalLogInDispatch,
        globalLogOutDispatch,
    };

    return <AuthContext.Provider value={ctx}>{children}</AuthContext.Provider>;
};

export default AuthContext;