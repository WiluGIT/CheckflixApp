import {
    flexRender,
    getCoreRowModel,
    getSortedRowModel,
    getPaginationRowModel,
    SortingState,
    useReactTable,
    PaginationState,
} from "@tanstack/react-table";
import React, { useEffect, useState } from "react";
import { productionColumnDefs } from "./ProductionColumnDefs";
import Pagination from "./Pagination";
import { useGetProductionsQuery } from "@/api/queries/production.query";
import useDebounce from "@/hooks/useDebounce";
import useAxiosApi from "@/hooks/useAxiosApi";
import ProductionModal from "../Modal/ProductionModal";

const Table = () => {
    const axiosApi = useAxiosApi();
    const [sorting, setSorting] = useState<SortingState>([]);
    const [searchTerm, setSearchTerm] = useState<string>('');
    const [openProductionModal, setOpenProductionModal] = useState(false);
    const handleModalToggle = () => { setOpenProductionModal((prev) => !prev); }
    const debouncedSearchTerm = useDebounce(searchTerm, 500);

    const [{ pageIndex, pageSize }, setPagination] =
        React.useState<PaginationState>({
            pageIndex: 0,
            pageSize: 10,
        })
    const pagination = React.useMemo(
        () => ({
            pageIndex,
            pageSize,
        }),
        [pageIndex, pageSize]
    );
    const fetchDataOptions = {
        pageNumber: pageIndex + 1,
        pageSize: pageSize,
        'AdvancedSearch.Fields': ['title'],
        'AdvancedSearch.Keyword': debouncedSearchTerm
    }

    const { data, isLoading, isFetching, isFetched } = useGetProductionsQuery(fetchDataOptions, { staleTime: (15 * 1000) });

    const table = useReactTable({
        data: data?.items ?? [],
        columns: productionColumnDefs,
        pageCount: data?.totalPages ?? -1,
        state: {
            pagination,
        },
        onPaginationChange: setPagination,
        getCoreRowModel: getCoreRowModel(),
        manualPagination: true,
        debugTable: true,
    })

    const headers = table.getFlatHeaders();
    const rows = table.getRowModel().rows;
    return (
        <div className="overflow-auto">
            <div className="flex justify-between gap-5">
                <div className={`bg-people-search-gradient text-white border border-solid border-zinc-300/50 rounded-lg max-h-11 w-[300px] relative  pt-[1px]`}>
                    <div className="flex w-full h-[42px] items-center">
                        <input
                            type='text'
                            value={searchTerm}
                            onChange={(e) => setSearchTerm(e.target.value)}
                            className='h-full w-full overflow-hidden border-none outline-none bg-[initial] px-4' placeholder='Search Productions'
                        />
                    </div>
                </div >
                <button className="btn btn-primary" onClick={() => handleModalToggle()}>Create</button>
            </div>
            <table className="table table-zebra my-4 w-full">
                <thead>
                    <tr>
                        {headers.map((header) => {
                            const direction = header.column.getIsSorted();
                            const arrow: any = {
                                asc: "🔼",
                                desc: "🔽",
                            };
                            const sort_indicator = direction && arrow[direction];
                            return (
                                <th key={header.id}>
                                    {header.isPlaceholder ? null : (
                                        <div
                                            onClick={header.column.getToggleSortingHandler()}
                                            className="cursor-pointer flex gap-4"
                                        >
                                            {flexRender(
                                                header.column.columnDef.header,
                                                header.getContext()
                                            )}
                                            {direction && <span>{sort_indicator}</span>}
                                        </div>
                                    )}
                                </th>
                            );
                        })}
                    </tr>
                </thead>
                <tbody>
                    {rows.map((row) => (
                        <tr key={row.id}>
                            {row.getVisibleCells().map((cell, index) => {
                                return index === 0 ? (
                                    <th key={cell.id}>
                                        {flexRender(cell.column.columnDef.cell, cell.getContext())}
                                    </th>
                                ) : (
                                    <td key={cell.id}>
                                        {flexRender(cell.column.columnDef.cell, cell.getContext())}
                                    </td>
                                );
                            })}
                        </tr>
                    ))}
                </tbody>
            </table>
            <Pagination table={table} />
            <ProductionModal open={openProductionModal} onClose={handleModalToggle} enableClickOutside={true} />
        </div>
    );
};

export default Table;
