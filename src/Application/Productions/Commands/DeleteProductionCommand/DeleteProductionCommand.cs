using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.Domain.Entities;
using MediatR;

namespace CheckflixApp.Application.Productions.Commands.DeleteProductionCommand;

public record DeleteProductionCommand(int Id) : IRequest<Result<string>>;