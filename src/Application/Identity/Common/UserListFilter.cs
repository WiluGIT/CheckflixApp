using CheckflixApp.Application.Common.Models;

namespace CheckflixApp.Application.Identity.Common;
public class UserListFilter : PaginationFilter
{
    public bool? IsActive { get; set; }
}