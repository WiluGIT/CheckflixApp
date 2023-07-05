namespace CheckflixApp.Application.ApplicationUserProductions.Common;
public class UserCollectionsDto
{
    public List<ProductionBasicDto> Favourites { get; set; } = new();
    public List<ProductionBasicDto> ToWatch { get; set; } = new();
    public List<ProductionBasicDto> Watched { get; set; } = new();
}