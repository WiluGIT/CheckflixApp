using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CheckflixApp.Application.Identity.Users.Commands.ToggleUserStatus;

public class ToggleUserStatusCommandHandler : IRequestHandler<ToggleUserStatusCommand, Result<Unit>>
{
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ToggleUserStatusCommandHandler(IUserService userService, IHttpContextAccessor httpContextAccessor)
    {
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<Unit>> Handle(ToggleUserStatusCommand command, CancellationToken cancellationToken)
    {
        var toggleStatusResult = await _userService.ToggleUserStatusAsync(command, cancellationToken);

        if (toggleStatusResult.IsFailure)
        {
            return toggleStatusResult.Errors;
        }

        return Unit.Value;
    }
}
