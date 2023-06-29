const Loader: React.FC<{ width: string }> = ({ width }) => {
    return (
        <div className="flex justify-center items-center h-full">
            <span className={`loading loading-infinity text-white ${width}`}></span>
        </div>
    );
}

export default Loader;