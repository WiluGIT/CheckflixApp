type GenreCardPropsType = {
    title: string;
    isActive: boolean;
}

const GenreCard: React.FC<GenreCardPropsType> = ({ title, isActive }) => {
    return (
        <div className={`transition-all duration-[500ms] ease-in-out rounded-lg border border-[#48484A] cursor-pointer bg-genre-card hover:bg-gray-100/50 ${isActive && 'bg-gray-100'}`}>
            <div className="flex items-center justify-center h-[50px] w-[137px]">
                <span className="text-center text-white">{title}</span>
            </div>
        </div>
    );
}

export default GenreCard;