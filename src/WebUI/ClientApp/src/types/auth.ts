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
