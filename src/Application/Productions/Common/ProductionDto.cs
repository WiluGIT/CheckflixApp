using AutoMapper;
using CheckflixApp.Application.Common.Mappings;
using CheckflixApp.Application.TodoLists.Queries.GetTodos;
using CheckflixApp.Domain.Entities;

namespace CheckflixApp.Application.Productions.Common;

public class ProductionDto : IMapFrom<Production>
{
    public int ProductionId { get; set; }
    public string TmdbId { get; set; } = string.Empty;
    public string ImdbId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public IEnumerable<string> Genres { get; set; } = new List<string>();
}
