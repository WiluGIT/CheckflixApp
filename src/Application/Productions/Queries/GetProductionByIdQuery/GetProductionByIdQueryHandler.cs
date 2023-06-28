using AutoMapper;
using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Productions.Common;
using CheckflixApp.Domain.Common.Primitives;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.Productions.Queries.GetProductionByIdQuery;

public class GetProductionByIdQueryHandler : IRequestHandler<GetProductionByIdQuery, Result<ProductionDto>>
{
    private readonly IStringLocalizer<GetProductionByIdQueryHandler> _localizer;
    private readonly IProductionRepository _productionRepository;
    private readonly IMapper _mapper;

    public GetProductionByIdQueryHandler(IStringLocalizer<GetProductionByIdQueryHandler> localizer, IProductionRepository productionRepository, IMapper mapper)
    {
        _localizer = localizer;
        _productionRepository = productionRepository;
        _mapper = mapper;
    }

    public async Task<Result<ProductionDto>> Handle(GetProductionByIdQuery request, CancellationToken cancellationToken)
    {
        var production = await _productionRepository.GetProductionDtoById(request.Id);

        if (production == null)
        {
            return Error.NotFound(description: _localizer["Production not found"]);
        }

        return production;
    }
}