using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.Productions.Commands.CreateProductionCommand;

public class CreateProductionCommandHandler : IRequestHandler<CreateProductionCommand, Result<string>>
{
    private readonly IStringLocalizer<CreateProductionCommandHandler> _localizer;
    private readonly IProductionRepository _productionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductionCommandHandler(IStringLocalizer<CreateProductionCommandHandler> localizer, IProductionRepository productionRepository, IUnitOfWork unitOfWork)
    {
        _localizer = localizer;
        _productionRepository = productionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(CreateProductionCommand command, CancellationToken cancellationToken)
    {
        var production = Production.Create(
            command.TmdbId, 
            command.ImdbId, 
            command.Title, 
            command.Overview, 
            command.Director, 
            command.Keywords, 
            new List<ProductionGenre>());

        _productionRepository.Insert(production);

        await _unitOfWork.SaveChangesAsync();

        return _localizer["Production has been added successfully"].Value;
    }
}