import { useRegisterMutation } from "@/api/queries/auth.query";
import { formatServerError } from "@/lib/helpers";
import { registerSchema } from "@/lib/validation";
import { ServerError } from "@/types/api";
import { zodResolver } from "@hookform/resolvers/zod";
import { SubmitHandler, useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

type RegisterInputs = {
    email: string,
    userName: string,
    password: string,
    confirmPassword: string
};

const Register = () => {
    const navigate = useNavigate();
    const {
        register,
        handleSubmit,
        formState: { errors, isValid }
    } = useForm<RegisterInputs>({ resolver: zodResolver(registerSchema) });
    const {
        isLoading,
        mutateAsync,
    } = useRegisterMutation({
        onSuccess: (response: string) => {
            toast.success("Account created successfully", { theme: 'colored' });
            navigate('/login');
        },
        onError: (error: ServerError) => {
            toast.error(formatServerError(error), { theme: 'colored' });
        }
    });

    const onSubmit: SubmitHandler<RegisterInputs> = async (data) => {
        await mutateAsync(data);
    }

    return (
        <form onSubmit={handleSubmit(onSubmit)} className="flex justify-center items-center h-full text-white">
            <div className="card flex-shrink-0 w-full max-w-sm shadow-2xl bg-base-100">
                <div className="card-body">
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

                    <button className="btn btn-primary mt-6" type="submit">
                        {isLoading && (<span className="loading loading-spinner"></span>)}
                        Register
                    </button>
                </div>
            </div>
        </form>
    );
};

export default Register;