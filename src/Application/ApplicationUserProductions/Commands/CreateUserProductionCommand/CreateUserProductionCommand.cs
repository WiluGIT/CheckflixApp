using CheckflixApp.Application.Common.Security;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.ApplicationUserProductions.Commands.CreateUserProductionCommand;

[Authorize]
public record CreateUserProductionCommand(int ProductionId, bool? Favourites, bool? ToWatch, bool? Watched) : IRequest<Result<string>>;