const Loader: React.FC<{ classes: string, loaderType: string }> = ({ classes, loaderType }) => {
    return (
        <div className="flex justify-center items-center h-full">
            <span className={`loading text-white ${loaderType} ${classes}`}></span>
        </div>
    );
}

export default Loader;