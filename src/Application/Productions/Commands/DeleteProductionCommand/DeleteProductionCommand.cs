using CheckflixApp.Application.Common.Security;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Productions.Commands.DeleteProductionCommand;

[Authorize(Roles = "Administrator")]
public record DeleteProductionCommand(int Id) : IRequest<Result<string>>;