using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Common.Models;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.Productions.Queries.GetProductionsQuery;
public class GetProductionsQueryHandler : IRequestHandler<GetProductionsQuery, Result<PaginatedList<Production>>>
{
    private readonly IStringLocalizer<GetProductionsQueryHandler> _localizer;
    private readonly IProductionRepository _productionRepository;

    public GetProductionsQueryHandler(IStringLocalizer<GetProductionsQueryHandler> localizer, IProductionRepository productionRepository)
    {
        _localizer = localizer;
        _productionRepository = productionRepository;
    }

    public async Task<Result<PaginatedList<Production>>> Handle(GetProductionsQuery request, CancellationToken cancellationToken)
        => await _productionRepository.GetAllProductions(request.filter);
}
