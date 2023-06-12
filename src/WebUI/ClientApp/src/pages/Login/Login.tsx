import { useLoginMutation } from "@/api/queries/auth.query";
import { formatServerError } from "@/lib/helpers";
import { loginSchema } from "@/lib/validation";
import { ServerError } from "@/types/api";
import { zodResolver } from "@hookform/resolvers/zod";
import { SubmitHandler, useForm } from "react-hook-form";
import { toast } from "react-toastify";

type LoginInputs = {
    email: string,
    password: string,
};


const Login = () => {
    const {
        register,
        handleSubmit,
        formState: { errors }
    } = useForm<LoginInputs>({ resolver: zodResolver(loginSchema) });
    const {
        isLoading,
        mutateAsync,
        data
    } = useLoginMutation();

    const onSubmit: SubmitHandler<LoginInputs> = async (data) => {
        await mutateAsync(data)
            .catch((error: ServerError) => {
                toast.error(formatServerError(error), { theme: 'colored' });
            });
    }

    return (
        <div>
            <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col justify-center items-center bg-300 mt-5 gap-5">
                <div className="form-control w-full max-w-xs">
                    <label className="label">
                        <span className="label-text text-white">Login</span>
                    </label>
                    <input type="text" placeholder="Type email" {...register("email", { required: true })} className="input input-bordered w-full max-w-xs" />
                    <span className="text-error">{errors.email?.message}</span>
                </div>

                <div className="form-control w-full max-w-xs">
                    <label className="label">
                        <span className="label-text text-white">Password</span>
                    </label>
                    <input type="password" placeholder="Type password" {...register("password", { required: true })} className="input input-bordered w-full max-w-xs" />
                    <span className="text-error">{errors.password?.message}</span>
                </div>

                <button className="btn" type="submit">
                    {isLoading && (<span className="loading loading-spinner"></span>)}
                    Log In
                </button>

                <span>{JSON.stringify(data)}</span>
            </form>
        </div>
    );
};

export default Login;