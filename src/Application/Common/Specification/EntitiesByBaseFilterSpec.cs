using Ardalis.Specification;
using CheckflixApp.Application.Common.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CheckflixApp.Application.Common.Specification;
public class EntitiesByBaseFilterSpec<T, TResult> : Specification<T, TResult>
{
    public EntitiesByBaseFilterSpec(BaseFilter filter) =>
        Query.SearchBy(filter);
}

public class EntitiesByBaseFilterSpec<T> : Specification<T>
{
    public EntitiesByBaseFilterSpec(BaseFilter filter) =>
        Query.SearchBy(filter);
}