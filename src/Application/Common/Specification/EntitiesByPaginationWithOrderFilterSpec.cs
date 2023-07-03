using CheckflixApp.Application.Common.Models;

namespace CheckflixApp.Application.Common.Specification;
public class EntitiesByPaginationWithOrderFilterSpec<T, TResult> : EntitiesByBaseFilterSpec<T, TResult>
{
    public EntitiesByPaginationWithOrderFilterSpec(PaginationFilter filter)
        : base(filter) =>
        Query.SearchByWithOrder(filter);
}

public class EntitiesByPaginationWithOrderFilterSpec<T> : EntitiesByBaseFilterSpec<T>
{
    public EntitiesByPaginationWithOrderFilterSpec(PaginationFilter filter)
        : base(filter) =>
        Query.SearchByWithOrder(filter);
}