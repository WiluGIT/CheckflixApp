using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Common.Models;
using CheckflixApp.Application.Productions.Common;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Genres.Queries.GetGenresProductionsQuery;

public class GetGenresProductionsQueryHandler : IRequestHandler<GetGenresProductionsQuery, Result<PaginatedList<ProductionDto>>>
{
    private readonly IGenreRepository _genreRepository;

    public GetGenresProductionsQueryHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<Result<PaginatedList<ProductionDto>>> Handle(GetGenresProductionsQuery request, CancellationToken cancellationToken)
        => await _genreRepository.GetGenresProductions(request.filter);
}
