using CheckflixApp.Application.Common.Models;
using CheckflixApp.Application.Productions.Common;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.Domain.Entities;
using MediatR;

namespace CheckflixApp.Application.Productions.Queries.GetProductionsQuery;

public record GetProductionsQuery(PaginationFilter filter) : IRequest<Result<PaginatedList<ProductionDto>>>;