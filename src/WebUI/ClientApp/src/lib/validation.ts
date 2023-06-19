import { z } from "zod";

export const loginSchema = z.object({
    email: z.string().nonempty().email(),
    password: z.string().nonempty(),
});

export const registerSchema = z.object({
    email: z.string().nonempty().email(),
    userName: z.string().nonempty(),
    password: z.string().min(6, "Password must contain at least 6 characters").regex(
        /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W)[a-zA-Z\d\W]+$/,
        'Password must contain at least one lowercase letter, one uppercase letter, one digit, and one non-alphanumeric character'
    ),
    confirmPassword: z.string().min(6, "Password must contain at least 6 characters").regex(
        /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W)[a-zA-Z\d\W]+$/,
        'Password must contain at least one lowercase letter, one uppercase letter, one digit, and one non-alphanumeric character'
    ),
}).refine((data) => data.password === data.confirmPassword, {
    message: "Passwords don't match",
    path: ["confirmPassword"],
});