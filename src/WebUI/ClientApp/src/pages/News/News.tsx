import ProductionList from "@/components/ProductionList/ProductionList";
import { ScrollToTop } from "@/components/ScrollToTop/ScrollToTop ";
import SectionHeading from "@/components/SectionHeading/SectionHeading";

const News = () => {
    return (
        <div>
            <SectionHeading text={`New TV Shows`} />
            <ProductionList filters={{ pageNumber: 1, pageSize: 40, orderBy: 'releaseDate desc' }} />
            <ScrollToTop />
        </div>
    );
}

export default News;