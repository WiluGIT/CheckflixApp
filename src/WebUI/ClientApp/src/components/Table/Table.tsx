import {
    flexRender,
    getCoreRowModel,
    getSortedRowModel,
    // 1. add necessary import
    getPaginationRowModel,
    SortingState,
    useReactTable,
} from "@tanstack/react-table";
import React, { useEffect } from "react";
import { productionColumnDefs } from "./ProductionColumnDefs";

//import { Person } from "../types/Person";
import Pagination from "./Pagination";
import { Production } from "@/types/production";
import { productions } from "@/mock/productionMock";
const Table = () => {
    const [sorting, setSorting] = React.useState<SortingState>([]);
    const table = useReactTable({
        columns: productionColumnDefs,
        data: productions as Production[],
        getCoreRowModel: getCoreRowModel(),
        getSortedRowModel: getSortedRowModel(),
        //2.  add getPaginationRowModel
        //getPaginationRowModel: getPaginationRowModel(),
        state: {
            sorting,
        },
        onSortingChange: setSorting,
    });

    useEffect(() => {
        console.log("Page changed: ", table.getState().pagination.pageIndex);

    }, [table.getState().pagination.pageIndex])

    const handlePagination = (page: any) => {
        console.log("Page changed: ", page);

    }

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
