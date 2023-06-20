export interface PaginationFilter extends BaseFilter {
    pageNumber: number;
    pageSize: number;
    orderBy?: string;
}

export interface BaseFilter {
    advancedSearch?: AdvancedSearch
    keyword?: string
}

export interface AdvancedSearch {
    fields: string[];
    keyword?: string;
}

export interface PaginationResponse<T> {
    items: T[];
    pageNumber: number;
    totalPages: number;
    totalCount: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
}
