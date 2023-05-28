namespace CheckflixApp.Domain.Entities;

public class ApplicationUserProduction : BaseAuditableEntity
{
    public string ApplicationUserId { get; set; }
    public int ProductionId { get; set; }
    public bool? Favourites { get; set; }
    public bool? ToWatch { get; set; }
    public bool? Watched { get; set; }


    private ApplicationUserProduction(
        string applicationUserId,
        int productionId,
        bool? favourites,
        bool? toWatch,
        bool? watched)
    {
        this.ApplicationUserId = applicationUserId;
        this.ProductionId = productionId;
        Favourites = favourites;
        ToWatch = toWatch;
        Watched = watched;
    }

    public static ApplicationUserProduction Create(
        string applicationUserId,
        int productionId,
        bool? favourites,
        bool? toWatch,
        bool? watched)
    {
        return new(
            applicationUserId: applicationUserId, 
            productionId: productionId, 
            favourites: favourites, 
            toWatch: toWatch, 
            watched: watched);
    }

    public string Update(
        bool? favourites,
        bool? toWatch,
        bool? watched)
    {
        string updateMessage = string.Empty;
        if (favourites.HasValue)
        {
            Favourites = favourites;
            updateMessage = favourites.Value ? "Added to 'Favourites'" : "Deleted from 'Favourites'";
        }

        if (toWatch.HasValue)
        {
            ToWatch = toWatch;
            updateMessage = toWatch.Value ? "Added to 'To Watch'" : "Deleted from 'To Watch'";
        }

        if (watched.HasValue)
        {
            Watched = watched;
            updateMessage = watched.Value ? "Added to 'To Watch'" : "Deleted from 'To Watch'";
        }

        return updateMessage;
    }

    #pragma warning disable CS8618 // Required by Entity Framework
    private ApplicationUserProduction() { }
}