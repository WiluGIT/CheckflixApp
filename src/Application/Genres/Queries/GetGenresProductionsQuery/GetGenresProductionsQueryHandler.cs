using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Genres.Common;
using CheckflixApp.Application.Productions.Common;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Genres.Queries.GetGenresProductionsQuery;

public class GetGenresProductionsQueryHandler : IRequestHandler<GetGenresProductionsQuery, Result<List<ProductionDto>>>
{
    private readonly IGenreRepository _genreRepository;

    public GetGenresProductionsQueryHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<Result<List<ProductionDto>>> Handle(GetGenresProductionsQuery request, CancellationToken cancellationToken)
        => await _genreRepository.GetGenresProductions(request.filter);
}
