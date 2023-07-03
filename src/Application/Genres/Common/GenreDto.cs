using CheckflixApp.Application.Common.Mappings;
using CheckflixApp.Domain.Entities;

namespace CheckflixApp.Application.Genres.Common;

public class GenreDto : IMapFrom<Genre>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
