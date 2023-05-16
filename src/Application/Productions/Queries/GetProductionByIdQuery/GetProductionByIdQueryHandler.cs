using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Followings.Common;
using CheckflixApp.Application.Followings.Queries.GetUserFollowingsCount;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Domain.Common.Primitives;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.Productions.Queries.GetProductionByIdQuery;

public class GetProductionByIdQueryHandler : IRequestHandler<GetProductionByIdQuery, Result<Production>>
{
    private readonly IStringLocalizer<GetProductionByIdQueryHandler> _localizer;
    private readonly IProductionRepository _productionRepository;

    public GetProductionByIdQueryHandler(IStringLocalizer<GetProductionByIdQueryHandler> localizer, IProductionRepository productionRepository)
    {
        _localizer = localizer;
        _productionRepository = productionRepository;
    }

    public async Task<Result<Production>> Handle(GetProductionByIdQuery request, CancellationToken cancellationToken)
    {
        var production = await _productionRepository.GetByIdAsync(request.Id);

        if (production == null)
        {
            return Error.NotFound(description: _localizer["Production not found"]);
        }

        return production;
    }
}