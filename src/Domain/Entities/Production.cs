namespace CheckflixApp.Domain.Entities;

public class Production : BaseAuditableEntity
{
    public string TmdbId { get; private set; }
    public string ImdbId { get; private set; }
    public string Title { get; private set; } 
    public string Overview { get; private set; }
    public string Director { get; private set; }
    public string Keywords { get; private set; }
    public DateTime ReleaseDate { get; private set; }


    private List<ProductionGenre> _productionGenres = new();
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
        DateTime releaseDate,
        List<ProductionGenre> productionGenres)
    {
        this.TmdbId = tmdbId;
        this.ImdbId = imdbId;
        this.Title = title;
        this.Overview = overview;
        this.Director = director;
        this.Keywords = keywords;
        this.ReleaseDate = releaseDate;
        this._productionGenres = productionGenres;
    }

    public static Production Create(
        string tmdbId,
        string imdbId,
        string title,
        string overview,
        string director,
        string keywords,
        string releaseDate,
        List<int> genres)
    {
        return new(
            tmdbId: tmdbId,
            imdbId: imdbId,
            title: title,
            overview: overview,
            director: director,
            keywords: keywords, 
            releaseDate: FormatProductionDate(releaseDate),
            productionGenres: CreateProductionGenres(genres));
    }

    public void Update(
        string tmdbId,
        string imdbId,
        string title,
        string overview,
        string director,
        string keywords,
        string releaseDate,
        List<int> genres)
    {
        this.TmdbId = tmdbId;
        this.ImdbId = imdbId;
        this.Title = title;
        this.Overview = overview;
        this.Director = director;
        this.Keywords = keywords;
        this.ReleaseDate = FormatProductionDate(releaseDate);
        this._productionGenres = CreateProductionGenres(genres);
    }

    private static List<ProductionGenre> CreateProductionGenres(List<int> genres) =>
        genres.Select(genreId => ProductionGenre.Create(genreId)).ToList();

    private static DateTime FormatProductionDate(string releaseDate)
    {
        if (DateTime.TryParse(releaseDate, out var dateValue))
        {
            return dateValue;
        }

        throw new ArgumentException("Unable to parse given Production date");
    }

#pragma warning disable CS8618 // Required by Entity Framework
    private Production() { }
}