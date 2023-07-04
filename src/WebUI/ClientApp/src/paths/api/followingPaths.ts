export const GET_USERS_WITH_FOLLOWING_PATH = '/Followings/users';
export const FOLLOW_USER_PATH = (userId: string) => `/Followings/${userId}`;
export const UNFOLLOW_USER_PATH = (userId: string) => `/Followings/${userId}`;