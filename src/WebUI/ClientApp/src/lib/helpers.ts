import { ServerError } from "@/types/api";

export const formatServerError = (error: ServerError): string => {
    return error.response?.data?.title || "Unexpected error";
};
