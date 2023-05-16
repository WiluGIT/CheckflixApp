using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Domain.Common.Primitives;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.Productions.Commands.DeleteProductionCommand;

public class DeleteProductionCommandHandler : IRequestHandler<DeleteProductionCommand, Result<string>>
{
    private readonly IStringLocalizer<DeleteProductionCommandHandler> _localizer;
    private readonly IProductionRepository _productionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductionCommandHandler(IStringLocalizer<DeleteProductionCommandHandler> localizer, IProductionRepository productionRepository, IUnitOfWork unitOfWork)
    {
        _localizer = localizer;
        _productionRepository = productionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteProductionCommand command, CancellationToken cancellationToken)
    {
        var production = await _productionRepository.GetByIdAsync(command.Id);

        if (production == null)
        {
            return Error.NotFound(description: _localizer["Production not found"]);
        }

        _productionRepository.Remove(production);

        await _unitOfWork.SaveChangesAsync();

        return _localizer["Production has been successfully deleted"].Value;
    }
}