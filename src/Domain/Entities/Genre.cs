namespace CheckflixApp.Domain.Entities;

public class Genre : BaseAuditableEntity
{
    public string Name { get; set; }

    private readonly List<ApplicationUserGenre> _applicationUserGenres = new();
    private readonly List<ProductionGenre> _productionGenres = new();
    public IReadOnlyList<ApplicationUserGenre> ApplicationUserGenres => _applicationUserGenres.AsReadOnly();
    public IReadOnlyList<ProductionGenre> ProductionGenres => _productionGenres.AsReadOnly();


    private Genre(string name)
    {
        this.Name = name;
    }

    public static Genre Create(string name)
    {
        return new(name: name);
    }

    #pragma warning disable CS8618 // Required by Entity Framework
    private Genre() { }
}