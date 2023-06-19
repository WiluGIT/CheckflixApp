using CheckflixApp.Application.Common.Security;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Productions.Commands.CreateProductionCommand;

[Authorize(Roles = "Administrator")]
public class CreateProductionCommand : IRequest<Result<string>>
{
    public string TmdbId { get; set; } = string.Empty;
    public string ImdbId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
    public string ReleaseDate { get; set; } = string.Empty;
    public List<int> GenreIds { get; set; } = new List<int>();
}
