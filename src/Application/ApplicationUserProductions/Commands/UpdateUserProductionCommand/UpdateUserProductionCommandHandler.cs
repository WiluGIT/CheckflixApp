using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Domain.Common.Primitives;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.ApplicationUserProductions.Commands.UpdateUserProductionCommand;

public class UpdateUserProductionCommandHandler : IRequestHandler<UpdateUserProductionCommand, Result<string>>
{
    private readonly IStringLocalizer<UpdateUserProductionCommandHandler> _localizer;
    private readonly IApplicationUserProductionRepository _applicationUserProductionRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserProductionCommandHandler(IStringLocalizer<UpdateUserProductionCommandHandler> localizer, IApplicationUserProductionRepository applicationUserProductionRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _localizer = localizer;
        _applicationUserProductionRepository = applicationUserProductionRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Result<string>> Handle(UpdateUserProductionCommand command, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId ?? string.Empty;
        var applicationUserProduction = await _applicationUserProductionRepository.GetByIdAsync(userId, command.ProductionId);

        if (applicationUserProduction == null)
        {
            return Error.NotFound(description: _localizer["User Production not found"]);
        }

        var updateMessage = applicationUserProduction.Update(
            command.Favourites,
            command.ToWatch,
            command.Watched);

        if (applicationUserProduction.Watched == false && applicationUserProduction.ToWatch == false && applicationUserProduction.Favourites == false)
        {
            _applicationUserProductionRepository.Remove(applicationUserProduction);
            await _unitOfWork.SaveChangesAsync();

            return _localizer["User Production has been removed successfully"].Value;
        }

        _applicationUserProductionRepository.Update(applicationUserProduction);
        await _unitOfWork.SaveChangesAsync();

        return _localizer[updateMessage].Value;
    }
}