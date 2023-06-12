import { useRegisterMutation } from "@/api/queries/auth.query";
import { formatServerError } from "@/lib/helpers";
import { registerSchema } from "@/lib/validation";
import { ServerError } from "@/types/api";
import { zodResolver } from "@hookform/resolvers/zod";
import { SubmitHandler, useForm } from "react-hook-form";
import { toast } from "react-toastify";

type RegisterInputs = {
    email: string,
    userName: string,
    password: string,
    confirmPassword: string
};

const Register = () => {
    const {
        register,
        handleSubmit,
        formState: { errors, isValid }
    } = useForm<RegisterInputs>({ resolver: zodResolver(registerSchema) });
    const {
        isLoading,
        mutateAsync,
        isSuccess,
    } = useRegisterMutation();

    const onSubmit: SubmitHandler<RegisterInputs> = async (data) => {
        await mutateAsync(data)
            .catch((error: ServerError) => {
                toast.error(formatServerError(error), { theme: 'colored' });
            });

        isSuccess && toast.success("Account created successfully", { theme: 'colored' });
    }

    return (
        <div>
            <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col justify-center items-center bg-300 mt-5 gap-5">
                <div className="form-control w-full max-w-xs">
                    <label className="label">
                        <span className="label-text text-white">Email</span>
                    </label>
                    <input type="text" placeholder="Type email" {...register("email", { required: true })} className="input input-bordered w-full max-w-xs" />
                    <span className="text-error">{errors.email?.message}</span>
                </div>

                <div className="form-control w-full max-w-xs">
                    <label className="label">
                        <span className="label-text text-white">User name</span>
                    </label>
                    <input type="text" placeholder="Type user name" {...register("userName", { required: true })} className="input input-bordered w-full max-w-xs" />
                    <span className="text-error">{errors.userName?.message}</span>
                </div>

                <div className="form-control w-full max-w-xs">
                    <label className="label">
                        <span className="label-text text-white">Password</span>
                    </label>
                    <input type="password" placeholder="Type password" {...register("password", { required: true })} className="input input-bordered w-full max-w-xs" />
                    <span className="text-error">{errors.password?.message}</span>
                </div>

                <div className="form-control w-full max-w-xs">
                    <label className="label">
                        <span className="label-text text-white">Confirm password</span>
                    </label>
                    <input type="password" placeholder="Type confirm password" {...register("confirmPassword", { required: true })} className="input input-bordered w-full max-w-xs" />
                    <span className="text-error">{errors.confirmPassword?.message}</span>
                </div>

                <button className="btn" type="submit">
                    {isLoading && (<span className="loading loading-spinner"></span>)}
                    Register
                </button>
            </form>
        </div>
    );
};

export default Register;