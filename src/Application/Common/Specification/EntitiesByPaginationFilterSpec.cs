using CheckflixApp.Application.Common.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CheckflixApp.Application.Common.Specification;
public class EntitiesByPaginationFilterSpec<T, TResult> : EntitiesByBaseFilterSpec<T, TResult>
{
    public EntitiesByPaginationFilterSpec(PaginationFilter filter)
        : base(filter) =>
        Query.PaginateBy(filter);
}

public class EntitiesByPaginationFilterSpec<T> : EntitiesByBaseFilterSpec<T>
{
    public EntitiesByPaginationFilterSpec(PaginationFilter filter)
        : base(filter) =>
        Query.PaginateBy(filter);
}