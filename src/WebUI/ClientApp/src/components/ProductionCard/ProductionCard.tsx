import { Production } from "@/types/production";

type ProductionCardPropsType = {
    production: Production
}

const ProductionCard: React.FC<ProductionCardPropsType> = ({ production }) => {
    return (
        <div className="card mx-auto w-[200px] duration-300 hover:-translate-y-1 mt-1">
            <span className="absolute top-0 text-white">{production.productionId}</span>
            <figure className="">
                <img src="https://image.tmdb.org/t/p/w500/vgpXmVaVyUL7GGiDeiK1mKEKzcX.jpg" className="rounded-lg shadow-lg" />
            </figure>
        </div>
    );
}

export default ProductionCard;