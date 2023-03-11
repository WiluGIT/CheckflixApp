namespace CheckflixApp.Domain.Entities;

public class ProductionGenre : BaseAuditableEntity
{
    public int GenreId { get; set; }
    public int ProductionId { get; set; }


    private ProductionGenre(
        int productionId,
        int genreId)
    {
        this.GenreId = genreId;
        this.ProductionId = productionId;
    }

    public static ProductionGenre Create(
        int productionId,
        int genreId)
    {
        return new(
            productionId: productionId, 
            genreId: genreId);
    }

    #pragma warning disable CS8618 // Required by Entity Framework
    private ProductionGenre() { }
}