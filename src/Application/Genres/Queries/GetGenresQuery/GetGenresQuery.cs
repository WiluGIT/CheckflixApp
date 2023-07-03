using CheckflixApp.Application.Genres.Common;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Genres.Queries.GetGenresQuery;

public record GetGenresQuery() : IRequest<Result<List<GenreDto>>>;