using CheckflixApp.Application.Identity.Interfaces;
using MediatR;

namespace CheckflixApp.Application.Identity.Roles.Commands.CreateOrUpdateRole;

public class CreateOrUpdateRoleCommandHandler : IRequestHandler<CreateOrUpdateRoleCommand, string>
{
    private readonly IRoleService _roleService;

    public CreateOrUpdateRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<string> Handle(CreateOrUpdateRoleCommand command, CancellationToken cancellationToken)
    {
        return await _roleService.CreateOrUpdateAsync(command);
    }
}
