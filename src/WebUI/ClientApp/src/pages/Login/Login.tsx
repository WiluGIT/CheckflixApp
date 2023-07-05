import { useLoginMutation } from "@/api/queries/auth.query";
import AuthContext from "@/context/AuthContextProvider";
import { formatServerError } from "@/lib/helpers";
import { loginSchema } from "@/lib/validation";
import { ServerError, ServerResponse } from "@/types/api";
import { LoginRequest, LoginResponse } from "@/types/auth";
import { zodResolver } from "@hookform/resolvers/zod";
import { useContext } from "react";
import { SubmitHandler, useForm } from "react-hook-form";
import { toast } from "react-toastify";
import { BsDiscord } from 'react-icons/bs';

type LoginInputs = {
    email: string,
    password: string,
};

const Login = () => {
    const {
        register,
        handleSubmit,
        formState: { errors, isValid }
    } = useForm<LoginInputs>({ resolver: zodResolver(loginSchema) });

    const {
        isLoading,
        mutateAsync,
    } = useLoginMutation({
        onSuccess: (response: LoginResponse) => {
            globalLogInDispatch(response);
        },
        onError: (error: ServerError) => {
            toast.error(formatServerError(error), { theme: 'colored' });
        }
    });

    const { globalLogInDispatch } = useContext(AuthContext);

    const onSubmit: SubmitHandler<LoginRequest> = async (data) => {
        await mutateAsync(data);
    }

    const discordLogin = () => {
        const clientID = import.meta.env.VITE_DISCORD_CLIENT_ID;
        const redirectURI = import.meta.env.VITE_AUTH_CALLBACK_URL;
        const scope = 'identify email';
        const discordAuthorizeURL = `https://discord.com/api/oauth2/authorize?client_id=${clientID}&redirect_uri=${encodeURIComponent(redirectURI)}&response_type=code&scope=${encodeURIComponent(scope)}`;

        window.open(discordAuthorizeURL, "_self");
    }

    return (
        <form onSubmit={handleSubmit(onSubmit)} className="flex justify-center items-center text-white">
            <div className="card flex-shrink-0 w-full max-w-sm shadow-2xl bg-base-100">
                <div className="card-body">
                    <div className="form-control">
                        <label className="label">
                            <span className="label-text">Email</span>
                        </label>
                        <input {...register("email", { required: true })} type="text" placeholder="Email" className="input input-bordered" />
                        <span className="text-error">{errors.email?.message}</span>
                    </div>
                    <div className="form-control">
                        <label className="label">
                            <span className="label-text">Password</span>
                        </label>
                        <input {...register("password", { required: true })} type="password" placeholder="Password" className="input input-bordered" />
                        <span className="text-error">{errors.password?.message}</span>
                        {/* <label className="label">
                            <a href="#" className="label-text-alt">Forgot password?</a>
                        </label> */}
                    </div>
                    <div className="form-control mt-6">
                        <button className="btn btn-primary" type="submit" disabled={!isValid}>
                            {isLoading && (<span className="loading loading-spinner"></span>)}
                            Log In
                        </button>

                        <button onClick={() => discordLogin()} type="button" className="btn btn-primary mt-6 bg-[#5865f2]">
                            <BsDiscord /> Log In with Discord
                        </button>
                    </div>
                </div>
            </div>
        </form>
    );
};

export default Login;