using CheckflixApp.Application.ApplicationUserProductions.Common;
using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.ApplicationUserProductions.Queries.GetUserProductionsQuery;

public class GetUserProductionsQueryHandler : IRequestHandler<GetUserProductionsQuery, Result<UserCollectionsDto>>
{
    private readonly IApplicationUserProductionRepository _applicationUserProductionRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetUserProductionsQueryHandler(IApplicationUserProductionRepository applicationUserProductionRepository, ICurrentUserService currentUserService)
    {
        _applicationUserProductionRepository = applicationUserProductionRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Result<UserCollectionsDto>> Handle(GetUserProductionsQuery query, CancellationToken cancellationToken) =>
        await _applicationUserProductionRepository.GetUserProductions(query.UserId);
}
