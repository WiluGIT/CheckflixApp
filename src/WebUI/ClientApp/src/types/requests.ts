export interface PaginationFilter extends BaseFilter {
    pageNumber: number;
    pageSize: number;
    orderBy?: string;
}

export interface BaseFilter {
    'AdvancedSearch.Fields'?: string | string[];
    'AdvancedSearch.Keyword'?: string;
    keyword?: string
}

export interface PaginationResponse<T> {
    items: T[];
    pageNumber: number;
    totalPages: number;
    totalCount: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
}
