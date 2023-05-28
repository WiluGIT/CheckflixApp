using CheckflixApp.Application.Productions.Common;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Productions.Queries.GetProductionByIdQuery;

public record GetProductionByIdQuery(int Id) : IRequest<Result<ProductionDto>>;