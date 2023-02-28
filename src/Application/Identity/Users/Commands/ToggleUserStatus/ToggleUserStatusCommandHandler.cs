using CheckflixApp.Application.Identity.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CheckflixApp.Application.Identity.Users.Commands.ToggleUserStatus;

public class ToggleUserStatusCommandHandler : IRequestHandler<ToggleUserStatusCommand, Unit>
{
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ToggleUserStatusCommandHandler(IUserService userService, IHttpContextAccessor httpContextAccessor)
    {
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Unit> Handle(ToggleUserStatusCommand command, CancellationToken cancellationToken)
    {
        await _userService.ToggleUserStatusAsync(command, cancellationToken);

        return Unit.Value;
    }
}
