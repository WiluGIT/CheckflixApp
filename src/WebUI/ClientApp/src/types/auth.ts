export interface LoginRequest {
    email: string;
    password: string;
}

export interface LoginResponse {
    token: string;
    refreshToken: string;
    refreshTokenExpiryDate: string;
}

export interface RegisterRequest {
    email: string,
    userName: string,
    password: string,
    confirmPassword: string
}

export interface AuthState {
    isLoggedIn: boolean;
    authToken?: string;
    userId?: string;
    name?: string;
    email?: string;
};

export type UserData = {
    authToken: string;
    userId: string;
    name: string;
    email: string;
};

export type AuthProviderProps = {
    children: React.ReactElement;
};

export type AuthContextType = {
    authState: AuthState;
    globalLogInDispatch: (props: UserData) => void;
    globalLogOutDispatch: () => void;
}
