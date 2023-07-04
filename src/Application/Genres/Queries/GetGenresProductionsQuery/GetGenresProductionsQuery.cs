using CheckflixApp.Application.Common.Models;
using CheckflixApp.Application.Genres.Common;
using CheckflixApp.Application.Productions.Common;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Genres.Queries.GetGenresProductionsQuery;

public record GetGenresProductionsQuery(GenresFilter filter) : IRequest<Result<PaginatedList<ProductionDto>>>;