namespace CheckflixApp.Domain.Entities;

public class ProductionGenre : BaseAuditableEntity
{
    public int GenreId { get; set; }
    public int ProductionId { get; set; }


    private ProductionGenre(
        int genreId)
    {
        this.GenreId = genreId;
    }

    public static ProductionGenre Create(
        int genreId)
    {
        return new(
            genreId: genreId);
    }

    #pragma warning disable CS8618 // Required by Entity Framework
    private ProductionGenre() { }
}