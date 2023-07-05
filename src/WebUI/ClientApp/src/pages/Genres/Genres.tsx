import { useGetGenresQuery } from "@/api/queries/genre.query";
import GenreCard from "@/components/GenreCard/GenreCard";
import GenreProductionList from "@/components/GenreProductionList/GenreProductionList";
import { ScrollToTop } from "@/components/ScrollToTop/ScrollToTop ";
import SectionHeading from "@/components/SectionHeading/SectionHeading";
import { useState } from "react";

const Genres = () => {
    const { data, isLoading, isSuccess } = useGetGenresQuery();
    const [selectedGenres, setSelectedGenres] = useState<number[]>([]);

    const handleGenreSelect = (selectedId: number) => {
        if (!selectedGenres.includes(selectedId)) {
            setSelectedGenres(prev => [...prev, selectedId]);
            return;
        }

        setSelectedGenres(selectedGenres.filter(item => item !== selectedId))
    }

    return (
        <div>
            <SectionHeading text={'Genres'} />
            <div className="flex flex-wrap gap-3 items-center">
                {isSuccess && data.map((el, idx) => (
                    <div onClick={() => handleGenreSelect(el.id)} key={el.id}>
                        <GenreCard title={el.name} isActive={selectedGenres.includes(el.id)} />
                    </div>
                ))}
            </div>

            <SectionHeading text={'Productions'} />
            <GenreProductionList filters={{ pageNumber: 1, pageSize: 40, genreIds: selectedGenres }} />
            <ScrollToTop />
        </div>
    );
}

export default Genres;