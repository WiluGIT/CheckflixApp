import { useState } from 'react';
import { AiOutlineSearch } from 'react-icons/ai';
import { IoMdClose } from 'react-icons/io';

const productions = [
    {
        "productionId": 48687,
        "title": "Toy Story",
        "releaseDate": "1995-10-30T00:00:00",
    },
    {
        "productionId": 48688,
        "title": "Jumanji",
        "releaseDate": "1995-12-15T00:00:00",
    },
    {
        "productionId": 48689,
        "title": "Grumpier Old Men",
        "releaseDate": "1995-12-22T00:00:00",
    },
    {
        "productionId": 48690,
        "title": "Waiting to Exhale",
        "releaseDate": "1995-12-22T00:00:00",
    },
]
const SearchBar = () => {
    const [isInputFocused, setIsInputFocused] = useState(false);

    const handleInputFocus = () => {
        setIsInputFocused(true);
    };

    const handleInputBlur = () => {
        setIsInputFocused(false);
    };

    return (
        <div className={`transition-all duration-[5000ms] bg-search-gradient text-white border border-solid border-gray-100 rounded-lg max-h-11 w-[300px] mt-5 relative rounded-b-none border-b-0 ${isInputFocused && "before:content-[''] before:h-[2px] before:left-0 before:absolute before:bottom-0 before:w-full before:z-50 before:bg-gray-100"}`}>
            <div className="flex w-full h-[42px] items-center">
                <div className='grow'>
                    <input
                        type='text'
                        className='h-full w-full overflow-hidden border-none outline-none bg-[initial] pl-4' placeholder='Search TV & Movies or People'
                        onFocus={handleInputFocus}
                        onBlur={handleInputBlur}
                    />
                </div>
                {isInputFocused && <div className='h-5 cursor-pointer text-xl -mr-1'>
                    <IoMdClose />
                </div>}
                <div className='h-6 mx-3 cursor-pointer text-2xl'>
                    <AiOutlineSearch />
                </div>
            </div>
            <div className={`transition-all duration-[500ms] ease-in-out relative pt-1 z-50 bg-search-gradient text-white border border-solid border-gray-100 rounded-lg rounded-t-none mt-0 -mx-[1px] border-t-0 ${isInputFocused ? 'max-h-[900px]' : 'max-h-0'} overflow-hidden `}>
                {productions.map((el, idx) => (
                    <a className='relative w-full flex items-center bg-[transparent] text-white py-2 px-5 h-[56px]'>
                        <div className='relative w-[27px] min-w-[27px] h-[40px] mr-4'>
                            <img src="https://image.tmdb.org/t/p/w500/vgpXmVaVyUL7GGiDeiK1mKEKzcX.jpg" className="rounded-lg shadow-lg" />
                        </div>
                        <span>{el.title}</span>
                    </a>
                ))}
            </div>
        </div>
    );
}

export default SearchBar;