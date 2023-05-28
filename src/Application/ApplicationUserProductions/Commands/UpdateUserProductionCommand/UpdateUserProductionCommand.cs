using CheckflixApp.Application.ApplicationUserProductions.Common;
using CheckflixApp.Application.Common.Security;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.ApplicationUserProductions.Commands.UpdateUserProductionCommand;

[Authorize]
public class UpdateUserProductionCommand : IRequest<Result<string>>
{
    public UpdateUserProductionCommand(int id, UpdateUserProductionRequest request)
    {
        ProductionId = id;
        Favourites= request.Favourites;
        ToWatch = request.ToWatch;
        Watched = request.Watched;
    }

    public int ProductionId { get; set; }
    public bool? Favourites { get; set; }
    public bool? ToWatch { get; set; }
    public bool? Watched { get; set; }
}
