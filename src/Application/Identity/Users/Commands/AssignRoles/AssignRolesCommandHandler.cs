using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Identity.Users.Commands.AssignRoles;

public class AssignRolesCommandHandler : IRequestHandler<AssignRolesCommand, Result<string>>
{
    private readonly IUserService _userService;
    public AssignRolesCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result<string>> Handle(AssignRolesCommand command, CancellationToken cancellationToken)
    {
        return await _userService.AssignRolesAsync(command.Id, command.UserRequest, cancellationToken);
    }
}
