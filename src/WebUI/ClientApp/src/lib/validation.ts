import { z } from "zod";

export const loginSchema = z.object({
    email: z.string().nonempty().email(),
    password: z.string().nonempty(),
});
