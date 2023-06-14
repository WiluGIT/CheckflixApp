import { UserData } from "@/types/auth"

export enum AuthActionEnum {
    LOG_IN = 'LOG_IN',
    LOG_OUT = 'LOG_OUT',
};

export type AuthAction = {
    type: AuthActionEnum.LOG_IN,
    payload: {
        userData: UserData
    }
} | {
    type: AuthActionEnum.LOG_OUT,
    payload: null,
}