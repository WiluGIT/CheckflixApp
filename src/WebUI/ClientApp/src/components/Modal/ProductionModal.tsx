import Modal, { ModalPropsType } from "./Modal";
import { GetFollowingResponse } from "@/types/following";
import { useUnFollowMutation } from "@/api/queries/followings.query";
import { toast } from "react-toastify";
import { ServerError } from "@/types/api";
import { formatServerError } from "@/lib/helpers";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { productionSchema } from "@/lib/validation";

type ProductionModalPropsType = ModalPropsType & {
    //data: GetFollowingResponse[] | undefined,
    //data: Production;
    refetch?(): void
}

type ModalInputs = {
    tmdbId: string;
    imdbId: string;
    title: string;
    overview: string;
    director: string;
    keywords: string;
    releaseDate: Date;
};

const ProductionModal: React.FC<ProductionModalPropsType> = ({ open, enableClickOutside, onClose, refetch }) => {
    const {
        register,
        handleSubmit,
        formState: { errors, isValid }
    } = useForm<ModalInputs>({ resolver: zodResolver(productionSchema) });
    const {
        isLoading,
        mutateAsync: mutateUnFollowAsync,
    } = useUnFollowMutation({
        onSuccess: (response: string) => {
            if (refetch) {
                refetch();
            }
            toast.success(response, { theme: 'colored' });
        },
        onError: (error: ServerError) => {
            toast.error(formatServerError(error), { theme: 'colored' });
        }
    });

    const handleUnFollow = async (userId: string) => {
        await mutateUnFollowAsync({ userId: userId });
    };

    return (
        <Modal open={open} onClose={onClose} enableClickOutside={enableClickOutside}>
            <div className="form-control w-full max-w-xs">
                <label className="label">
                    <span className="label-text text-white">Email</span>
                </label>
                <input type="text" placeholder="Type email" {...register("tmdbId", { required: true })} className="input input-bordered w-full max-w-xs" />
                <span className="text-error">{errors.tmdbId?.message}</span>
            </div>

            <div className="form-control w-full max-w-xs">
                <label className="label">
                    <span className="label-text text-white">User name</span>
                </label>
                <input type="text" placeholder="Type user name" {...register("imdbId", { required: true })} className="input input-bordered w-full max-w-xs" />
                <span className="text-error">{errors.imdbId?.message}</span>
            </div>

            <div className="form-control w-full max-w-xs">
                <label className="label">
                    <span className="label-text text-white">Password</span>
                </label>
                <input type="password" placeholder="Type password" {...register("title", { required: true })} className="input input-bordered w-full max-w-xs" />
                <span className="text-error">{errors.title?.message}</span>
            </div>

            <div className="form-control w-full max-w-xs">
                <label className="label">
                    <span className="label-text text-white">Confirm password</span>
                </label>
                <input type="password" placeholder="Type confirm password" {...register("overview", { required: true })} className="input input-bordered w-full max-w-xs" />
                <span className="text-error">{errors.overview?.message}</span>
            </div>

            <div className="form-control w-full max-w-xs">
                <label className="label">
                    <span className="label-text text-white">Confirm password</span>
                </label>
                <input type="password" placeholder="Type confirm password" {...register("director", { required: true })} className="input input-bordered w-full max-w-xs" />
                <span className="text-error">{errors.director?.message}</span>
            </div>

            <div className="form-control w-full max-w-xs">
                <label className="label">
                    <span className="label-text text-white">Confirm password</span>
                </label>
                <input type="password" placeholder="Type confirm password" {...register("keywords", { required: true })} className="input input-bordered w-full max-w-xs" />
                <span className="text-error">{errors.keywords?.message}</span>
            </div>

            <div className="form-control w-full max-w-xs">
                <label className="label">
                    <span className="label-text text-white">Confirm password</span>
                </label>
                <input type="password" placeholder="Type confirm password" {...register("releaseDate", { required: true })} className="input input-bordered w-full max-w-xs" />
                <span className="text-error">{errors.releaseDate?.message}</span>
            </div>

            <button className="btn btn-primary mt-6" type="submit">
                {isLoading && (<span className="loading loading-spinner"></span>)}
                Create
            </button>
        </Modal>
    );
}

export default ProductionModal;