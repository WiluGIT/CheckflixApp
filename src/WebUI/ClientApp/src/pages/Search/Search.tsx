import ProductionList from "@/components/ProductionList/ProductionList";
import { ScrollToTop } from "@/components/ScrollToTop/ScrollToTop ";
import SectionHeading from "@/components/SectionHeading/SectionHeading";
import { useEffect, useState } from "react";
import { useSearchParams } from "react-router-dom";

const Search = () => {
    const [searchParams, setSearchParams] = useSearchParams();
    const [searchTerm, setSearchTerm] = useState<string>('');

    useEffect(() => {
        setSearchTerm(searchParams.get("query") || '');
    }, [searchParams])

    return (
        <div>
            <SectionHeading text={`Searching for "${searchTerm}"...`} />
            <ProductionList filters={{ pageNumber: 1, pageSize: 40, 'AdvancedSearch.Fields': ['title', 'director'], 'AdvancedSearch.Keyword': searchTerm }} />
            <ScrollToTop />
        </div>
    );
}

export default Search;