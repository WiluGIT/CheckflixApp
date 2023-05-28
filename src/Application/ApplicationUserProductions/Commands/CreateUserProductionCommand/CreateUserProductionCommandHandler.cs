using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Productions.Commands.CreateProductionCommand;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.ApplicationUserProductions.Commands.CreateUserProductionCommand;

public class CreateUserProductionCommandHandler : IRequestHandler<CreateUserProductionCommand, Result<string>>
{
    private readonly IStringLocalizer<CreateUserProductionCommandHandler> _localizer;
    private readonly IApplicationUserProductionRepository _applicationUserProductionRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserProductionCommandHandler(IStringLocalizer<CreateUserProductionCommandHandler> localizer, IApplicationUserProductionRepository applicationUserProductionRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _localizer = localizer;
        _applicationUserProductionRepository = applicationUserProductionRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Result<string>> Handle(CreateUserProductionCommand command, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId ?? string.Empty;
        var applicationUserProduction = ApplicationUserProduction.Create(
            userId,
            command.ProductionId, 
            command.Favourites, 
            command.ToWatch, 
            command.Watched);

        _applicationUserProductionRepository.Insert(applicationUserProduction);

        await _unitOfWork.SaveChangesAsync();

        return _localizer["User Production has been added successfully"].Value;
    }
}