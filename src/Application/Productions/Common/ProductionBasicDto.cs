using CheckflixApp.Application.Common.Mappings;
using CheckflixApp.Domain.Entities;

public class ProductionBasicDto : IMapFrom<Production>
{
    public int ProductionId { get; set; }
    public string TmdbId { get; set; } = string.Empty;
    public string ImdbId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
}
