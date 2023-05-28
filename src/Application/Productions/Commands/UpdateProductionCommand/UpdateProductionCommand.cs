using CheckflixApp.Application.Common.Security;
using CheckflixApp.Application.Productions.Common;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Productions.Commands.UpdateProductionCommand;

[Authorize(Roles = "Administrator")]
public class UpdateProductionCommand : IRequest<Result<string>>
{
    public UpdateProductionCommand(int id, UpdateProductionRequest request)
    {
        Id = id;
        TmdbId = request.TmdbId;
        ImdbId = request.ImdbId;
        Title = request.Title;
        Overview = request.Overview;
        Director = request.Director;
        Keywords = request.Keywords;
        GenreIds = request.GenreIds;
    }

    public int Id { get; set; }
    public string TmdbId { get; set; } = string.Empty;
    public string ImdbId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
    public List<int> GenreIds { get; set; } = new List<int>();
}
