export interface LoginRequest {
    email: string;
    password: string;
}

export interface LoginResponse {
    token: string;
}

export interface RefreshTokenRequest {
    token: string;
}

export interface RefreshTokenResponse {
    token: string;
}

export interface RegisterRequest {
    email: string,
    userName: string,
    password: string,
    confirmPassword: string
}

// export interface AuthState {
//     isLoggedIn: boolean;
//     accessToken?: string;
//     id?: string;
//     userName?: string;
//     email?: string;
// };

export interface AuthState {
    isAuthenticated: boolean;
    user?: UserData;
};

export type UserData = {
    accessToken: string;
    id: string;
    userName: string;
    email: string;
    imageUrl: string;
    roles: string[];
};

export type AuthProviderProps = {
    children: React.ReactElement;
};

export type AuthContextType = {
    authState: AuthState;
    globalLogInDispatch: (props: UserData, withRedirect?: boolean) => void;
    globalLogOutDispatch: () => void;
}
