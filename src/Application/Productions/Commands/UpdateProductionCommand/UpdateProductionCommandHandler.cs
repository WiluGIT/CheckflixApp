using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Domain.Common.Primitives;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.Productions.Commands.UpdateProductionCommand;

public class UpdateProductionCommandHandler : IRequestHandler<UpdateProductionCommand, Result<string>>
{
    private readonly IStringLocalizer<UpdateProductionCommandHandler> _localizer;
    private readonly IProductionRepository _productionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductionCommandHandler(IStringLocalizer<UpdateProductionCommandHandler> localizer, IProductionRepository productionRepository, IUnitOfWork unitOfWork)
    {
        _localizer = localizer;
        _productionRepository = productionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdateProductionCommand command, CancellationToken cancellationToken)
    {
        var productionToUpdate = await _productionRepository.GetByIdAsync(command.Id);

        if (productionToUpdate == null)
        {
            return Error.NotFound(description: _localizer["Production not found"]);
        }

        productionToUpdate.Update(
            command.TmdbId,
            command.ImdbId,
            command.Title,
            command.Overview,
            command.Director,
            command.Keywords,
            command.ReleaseDate,
            command.GenreIds);

        _productionRepository.Update(productionToUpdate);

        await _unitOfWork.SaveChangesAsync();

        return _localizer["Production has been updated successfully"].Value;
    }
}