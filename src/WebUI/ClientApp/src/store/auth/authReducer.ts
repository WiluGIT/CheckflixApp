import { Reducer } from "react";
import { AuthAction } from "./authActions";
import { AuthState } from "@/types/auth";
import { defaultAuthState } from "@/context/AuthContextProvider";

const authReducer: Reducer<AuthState, AuthAction> = (state, action) => {
    if (action.type === "LOG_IN") {
        localStorage.setItem("user", JSON.stringify(action.payload));
        return {
            ...state,
            isLoggedIn: true,
            authToken: action.payload.authToken,
            userId: action.payload.userId,
            name: action.payload.name,
            email: action.payload.email,
        };
    }

    if (action.type === "LOG_OUT") {
        localStorage.removeItem("user");
        return defaultAuthState;
    }

    return defaultAuthState;
};

export default authReducer;