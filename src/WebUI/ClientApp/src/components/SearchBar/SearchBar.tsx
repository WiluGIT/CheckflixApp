import { useFakeSearchProductionsQuery } from '@/api/queries/production.query';
import useDebounce from '@/hooks/useDebounce';
import { productions } from '@/mock/productionMock';
import { useState } from 'react';
import { AiOutlineSearch } from 'react-icons/ai';
import { IoMdClose } from 'react-icons/io';
import Loader from '../Loader/Loader';


const SearchBar = () => {
    const [isInputFocused, setIsInputFocused] = useState<boolean>(false);
    const [searchTerm, setSearchTerm] = useState<string>('');
    const debouncedSearchTerm = useDebounce(searchTerm, 500);
    const { data, isLoading, isFetching, isFetched } = useFakeSearchProductionsQuery({ searchTerm: debouncedSearchTerm });
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

    const dropDownContent = () => {
        if (isFetching || !isFetched) {
            return <Loader classes="w-8 my-3" loaderType={'loading-dots'} />
        }

        if (!data?.length) {
            return <div className='w-full flex justify-center items-center my-3'>No search results</div>
        }

        return data?.map((el, idx) => (
            <a className='relative w-full flex items-center bg-[transparent] text-white py-2 px-5 h-[56px] cursor-pointer hover:text-white hover:bg-white/50'>
                <div className='relative w-[27px] min-w-[27px] h-[40px] mr-4'>
                    <img src="https://image.tmdb.org/t/p/w500/vgpXmVaVyUL7GGiDeiK1mKEKzcX.jpg" className="rounded-lg shadow-lg" />
                </div>
                <span>{el.title}</span>
            </a>
        ))
    };

    return (
        <div className={`transition-all duration-[5000ms] bg-search-gradient text-white border border-solid border-gray-100 rounded-lg max-h-11 w-[300px] mt-5 relative rounded-b-none border-b-0 ${hasMinLengthAndFocus() && "before:content-[''] before:h-[2px] before:left-0 before:absolute before:bottom-0 before:w-full before:z-50 before:bg-gray-100"}`}>
            <div className="flex w-full h-[42px] items-center">
                <div className='grow'>
                    <input
                        type='text'
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                        className='h-full w-full overflow-hidden border-none outline-none bg-[initial] pl-4' placeholder='Search TV & Movies or People'
                        onFocus={handleInputFocus}
                        onBlur={handleInputBlur}
                    />
                </div>
                {hasMinLength() && <div onClick={() => handleInputClear()} className='h-5 cursor-pointer text-xl -mr-1 z-50'>
                    <IoMdClose />
                </div>}
                <div className='h-6 mx-3 cursor-pointer text-2xl'>
                    <AiOutlineSearch />
                </div>
            </div>
            <div className={`transition-all duration-[500ms] ease-in-out relative z-50 bg-search-gradient pt-[5px] text-white border border-solid border-gray-100 rounded-lg rounded-t-none mt-0 -mx-[1px] border-t-0 ${hasMinLengthAndFocus() ? 'max-h-[900px]' : 'max-h-0'} overflow-hidden`}>
                {dropDownContent()}
            </div>

        </div >
    );
}

export default SearchBar;