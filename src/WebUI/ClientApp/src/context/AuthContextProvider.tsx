import {
    createContext,
    useReducer,
    useCallback,
    useEffect,
    useMemo,
} from "react";
import { useNavigate } from "react-router-dom";

import { AuthActionEnum } from "../store/auth/authActions";
import authReducer from "../store/auth/authReducer";
import { AuthContextType, AuthProviderProps, AuthState, UserData } from "@/types/auth";
import { toast } from "react-toastify";

export const defaultAuthState: AuthState = {
    isAuthenticated: false,
    user: undefined
};

const initializeState = (defaultAuthState: AuthState) => {
    // Browser refresh init call
    const user = localStorage.getItem("user");
    if (!user) {
        return defaultAuthState;
    }

    const userData: UserData = JSON.parse(user!);
    return {
        isAuthenticated: true,
        user: userData
    };
};

export const AuthContext = createContext<AuthContextType>({
    authState: defaultAuthState,
    globalLogInDispatch: () => { },
    globalLogOutDispatch: () => { },
});

export const AuthContextProvider = (props: AuthProviderProps) => {
    const { children } = props;
    const [authState, authDispatch] = useReducer(authReducer, defaultAuthState, initializeState);
    const navigate = useNavigate();

    const globalLogInDispatch = useCallback((userData: UserData, withRedirect = true) => {
        authDispatch({
            type: AuthActionEnum.LOG_IN,
            payload: {
                userData
            },
        });

        withRedirect && navigate("/");
        toast.success("Sucessfully logged in", { theme: 'colored' });
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