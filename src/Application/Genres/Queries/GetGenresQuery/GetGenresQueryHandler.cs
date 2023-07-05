using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Genres.Common;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Genres.Queries.GetGenresQuery;

public class GetGenresQueryHandler : IRequestHandler<GetGenresQuery, Result<List<GenreDto>>>
{
    private readonly IGenreRepository _genreRepository;

    public GetGenresQueryHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<Result<List<GenreDto>>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
        => await _genreRepository.GetAllGenres();
}
