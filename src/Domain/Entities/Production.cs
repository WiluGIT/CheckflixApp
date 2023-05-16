namespace CheckflixApp.Domain.Entities;
public class Production : BaseAuditableEntity
{
    public string TmdbId { get; set; }
    public string ImdbId { get; set; }
    public string Title { get; set; } 
    public string Overview { get; set; }
    public string Director { get; set; }
    public string Keywords { get; set; }


    private readonly List<ProductionGenre> _productionGenres = new();
    private readonly List<ApplicationUserProduction> _applicationUserProductions = new();
    public IReadOnlyList<ProductionGenre> ProductionGenres => _productionGenres.AsReadOnly();
    public IReadOnlyList<ApplicationUserProduction> ApplicationUserProductions => _applicationUserProductions.AsReadOnly();
    
    private Production(
        string tmdbId,
        string imdbId,
        string title,
        string overview,
        string director,
        string keywords,
        List<ProductionGenre> productionGenres)
    {
        this.TmdbId = tmdbId;
        this.ImdbId = imdbId;
        this.Title = title;
        this.Overview = overview;
        this.Director = director;
        this.Keywords = keywords;
        this._productionGenres = productionGenres;
    }

    public static Production Create(
        string tmdbId,
        string imdbId,
        string title,
        string overview,
        string director,
        string keywords,
        List<int> genres)
    {
        return new(
            tmdbId: tmdbId,
            imdbId: imdbId,
            title: title,
            overview: overview,
            director: director,
            keywords: keywords, 
            productionGenres: CreateProductionGenres(genres));
    }

    private static List<ProductionGenre> CreateProductionGenres(List<int> genres) =>
        genres.Select(genreId => ProductionGenre.Create(genreId)).ToList();

#pragma warning disable CS8618 // Required by Entity Framework
    private Production() { }
}