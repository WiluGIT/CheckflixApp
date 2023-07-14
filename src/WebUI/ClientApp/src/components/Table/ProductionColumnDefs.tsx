import { ColumnDef, createColumnHelper } from "@tanstack/react-table";
// import { Person } from "../types/Person";
import { BasicProduction, Production } from "@/types/production";

const columnHelper = createColumnHelper<BasicProduction>();

export const productionColumnDefs: ColumnDef<BasicProduction, any>[] = [
    columnHelper.accessor((row) => row.productionId, {
        id: "productionId",
        cell: (info) => info.getValue(),
        header: () => <span>Id</span>,
    }),
    columnHelper.accessor((row) => row.tmdbId, {
        id: "tmdbId",
        cell: (info) => <span>{info.getValue()}</span>,
        header: () => <span>Tmdb Id</span>,
    }),
    columnHelper.accessor((row) => row.imdbId, {
        id: "imdbId",
        cell: (info) => <span>{info.getValue()}</span>,
        header: () => <span>Imdb Id</span>,
    }),
    columnHelper.accessor((row) => row.title, {
        id: "title",
        cell: (info) => <span>{info.getValue()}</span>,
        header: () => <span>Title</span>,
    }),
    columnHelper.accessor((row) => row.releaseDate, {
        id: "releaseDate",
        cell: (info) => <span>{info.getValue().toString()}</span>,
        header: () => <span>Release Date</span>,
    }),
];
