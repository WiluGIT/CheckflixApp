import { useGetProductionsQuery } from '@/api/queries/production.query';
import useDebounce from '@/hooks/useDebounce';
import { useState } from 'react';
import { AiOutlineSearch } from 'react-icons/ai';
import { IoMdClose } from 'react-icons/io';
import Loader from '../Loader/Loader';
import { NavLink, createSearchParams, useNavigate } from 'react-router-dom';


const SearchBar = () => {
    const [isInputFocused, setIsInputFocused] = useState<boolean>(false);
    const [searchTerm, setSearchTerm] = useState<string>('');
    const debouncedSearchTerm = useDebounce(searchTerm, 500);
    const { data, isLoading, isFetching, isFetched } = useGetProductionsQuery({
        pageNumber: 1,
        pageSize: 5,
        'AdvancedSearch.Fields': ['title', 'director'],
        'AdvancedSearch.Keyword': debouncedSearchTerm
    },
        debouncedSearchTerm != ''
    );

    const navigate = useNavigate();
    const minTermLength = 2

    const handleInputFocus = () => {
        setIsInputFocused(true);
    };

    const handleInputBlur = () => {
        setIsInputFocused(false);
    };

    const handleInputClear = () => {
        setSearchTerm('');
    }

    const hasMinLength = () => {
        return searchTerm.length >= minTermLength;
    }

    const hasMinLengthAndFocus = () => {
        return hasMinLength() && isInputFocused;
    }

    const handleInputSubmit = (e: React.SyntheticEvent) => {
        e.preventDefault();
        setIsInputFocused(false);
        navigate({ pathname: '/search', search: `?${createSearchParams({ query: searchTerm })}` })
    }

    const dropDownContent = () => {
        if (isFetching || !isFetched) {
            return <Loader classes="w-8 my-3" loaderType={'loading-dots'} />
        }

        if (!data?.items.length) {
            return <div className='w-full flex justify-center items-center my-3'>No search results</div>
        }

        return <div>
            {data?.items.map((el, idx) => (
                <a key={idx} className='w-full flex items-center text-white py-2 px-5 h-[56px] cursor-pointer hover:text-white hover:bg-white/50'>
                    <div className='relative w-[27px] min-w-[27px] h-[40px] mr-4'>
                        <img src="https://image.tmdb.org/t/p/w500/vgpXmVaVyUL7GGiDeiK1mKEKzcX.jpg" className="rounded-lg shadow-lg" />
                    </div>
                    <span className='line-clamp-2'>{el.title}</span>
                </a>
            ))}
            <NavLink to={{ pathname: '/search', search: `?${createSearchParams({ query: searchTerm })}` }}>
                <div className='flex justify-center cursor-pointer py-2 hover:bg-white/50'>See all</div>
            </NavLink>

        </div>
    };

    return (
        <form onSubmit={(e) => handleInputSubmit(e)} className={`bg-search-gradient text-white border border-solid border-zinc-300/50 rounded-lg max-h-11 w-[300px] mt-9 relative rounded-b-none border-b-0 pt-[1px] ${hasMinLengthAndFocus() && "before:content-[''] before:h-[1px] before:left-0 before:absolute before:bottom-0 before:w-full before:z-50 before:bg-zinc-100/50"}`}>
            <div className="flex w-full h-[42px] items-center">
                <div className='grow mt-[1px]'>
                    <input
                        type='text'
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                        className='h-full w-full overflow-hidden border-none outline-none bg-[initial] pl-4' placeholder='Search TV & Movies'
                        onFocus={handleInputFocus}
                        onBlur={handleInputBlur}
                    />
                </div>
                {hasMinLength() && <div onClick={() => handleInputClear()} className='h-5 cursor-pointer text-xl -mr-1 z-50 mt-[1px]'>
                    <IoMdClose />
                </div>}
                <div className='h-6 mx-3 cursor-pointer text-2xl  mt-[1px]' onClick={(e) => handleInputSubmit(e)}>
                    <AiOutlineSearch />
                </div>
            </div>
            <div className={`transition-max-h duration-[500ms] ease-in-out relative z-50 bg-search-gradient text-white border border-solid border-zinc-300/50 rounded-lg rounded-t-none mt-0 -mx-[1px] border-t-0 ${hasMinLengthAndFocus() ? 'max-h-[900px] ' : 'max-h-0 pt-[5px]'} overflow-hidden`}>
                {dropDownContent()}
            </div>

        </form >
    );
}

export default SearchBar;