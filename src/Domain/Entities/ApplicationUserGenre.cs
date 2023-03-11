namespace CheckflixApp.Domain.Entities;

public class ApplicationUserGenre : BaseAuditableEntity
{
    public string ApplicationUserId { get; set; }
    public int GenreId { get; set; }


    private ApplicationUserGenre(
        string applicationUserId,
        int genreId)
    {
        this.ApplicationUserId = applicationUserId;
        this.GenreId = genreId;
    }

    public static ApplicationUserGenre Create(
        string applicationUserId,
        int genreId)
    {
        return new(
            applicationUserId: applicationUserId,
            genreId: genreId);
    }

    #pragma warning disable CS8618 // Required by Entity Framework
    private ApplicationUserGenre() { }
}