import {
    flexRender,
    getCoreRowModel,
    getSortedRowModel,
    getPaginationRowModel,
    SortingState,
    useReactTable,
    PaginationState,
} from "@tanstack/react-table";
import React, { useEffect } from "react";
import { productionColumnDefs } from "./ProductionColumnDefs";
import Pagination from "./Pagination";
import { useGetProductionsQuery } from "@/api/queries/production.query";
const Table = () => {
    const [sorting, setSorting] = React.useState<SortingState>([]);
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
    )

    const fetchDataOptions = {
        pageNumber: pageIndex + 1,
        pageSize: pageSize,
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
        </div>
    );
};

export default Table;
