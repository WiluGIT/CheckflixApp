using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.Domain.Entities;
using MediatR;

namespace CheckflixApp.Application.ApplicationUserProductions.Queries.GetUserProductionsQuery;

public class GetUserProductionsQueryHandler : IRequestHandler<GetUserProductionsQuery, Result<List<Production>>>
{
    private readonly IApplicationUserProductionRepository _applicationUserProductionRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetUserProductionsQueryHandler(IApplicationUserProductionRepository applicationUserProductionRepository, ICurrentUserService currentUserService)
    {
        _applicationUserProductionRepository = applicationUserProductionRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Result<List<Production>>> Handle(GetUserProductionsQuery query, CancellationToken cancellationToken) =>
        await _applicationUserProductionRepository.GetUserProductions(_currentUserService.UserId ?? string.Empty);
}
