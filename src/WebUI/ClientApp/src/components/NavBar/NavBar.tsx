import SearchBar from "@/components/SearchBar/SearchBar.tsx";

const NavBar = () => {
    return (
        <nav className="flex flex-row w-full h-[80px] justify-center">
            <SearchBar />
        </nav>
    );
}

export default NavBar;