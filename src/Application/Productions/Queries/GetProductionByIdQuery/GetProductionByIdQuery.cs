using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.Domain.Entities;
using MediatR;

namespace CheckflixApp.Application.Productions.Queries.GetProductionByIdQuery;

public record GetProductionByIdQuery(int Id) : IRequest<Result<Production>>;