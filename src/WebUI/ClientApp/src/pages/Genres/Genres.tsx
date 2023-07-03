import GenreCard from "@/components/GenreCard/GenreCard";
import ProductionList from "@/components/ProductionList/ProductionList";
import { ScrollToTop } from "@/components/ScrollToTop/ScrollToTop ";
import SectionHeading from "@/components/SectionHeading/SectionHeading";
import { useState } from "react";

const genres = [
    "All genres",
    "Action",
    "Adventure",
    "Animation",
    "Comedy",
    "Crime",
    "Documentary",
    "Drama",
    "Family",
    "Fantasy",
    "Fiction",
    "Foreign",
    "History",
    "Horror",
    "Movie"
];

const Genres = () => {
    const [selectedGenres, setSelectedGenres] = useState<string[]>([]);

    const handleGenreSelect = (selectedId: string) => {
        if (!selectedGenres.includes(selectedId)) {
            setSelectedGenres(prev => [...prev, selectedId]);
            return;
        }

        setSelectedGenres(selectedGenres.filter(item => item !== selectedId))
        if (selectedGenres.length === 1) {
            setSelectedGenres(prev => [...prev, genres[0]]);
        }

    }

    return (
        <div>
            <SectionHeading text={`Genres`} />
            <div className="flex flex-wrap gap-3 items-center">
                {genres.map((el, idx) => (
                    <div onClick={() => handleGenreSelect(el)} key={idx}>
                        <GenreCard title={el} isActive={selectedGenres.includes(el)} />
                    </div>
                ))}
            </div>

            <SectionHeading text={`All genres`} />
            <ProductionList filters={{ pageNumber: 1, pageSize: 40, orderBy: 'releaseDate desc' }} />
            <ScrollToTop />
        </div>
    );
}

export default Genres;