type SectionHeadingPropsType = {
    text: string;
}

const SectionHeading: React.FC<SectionHeadingPropsType> = ({ text }) => {
    return (
        <div className="prose prose-slate lg:prose-lg text-white my-5">
            <h2 className="text-white ">{text}</h2>
        </div>
    );
}

export default SectionHeading;