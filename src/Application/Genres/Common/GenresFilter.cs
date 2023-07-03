using CheckflixApp.Application.Common.Models;

namespace CheckflixApp.Application.Genres.Common;
public class GenresFilter : PaginationFilter
{
    public List<int> GenreIds { get; set; } = new List<int>();
}
