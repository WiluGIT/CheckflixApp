using CheckflixApp.Application.Identity.Interfaces;
using MediatR;

namespace CheckflixApp.Application.Identity.Roles.Commands.DeleteRole;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, string>
{
    private readonly IRoleService _roleService;

    public DeleteRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<string> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
    {
        return await _roleService.DeleteAsync(command.Id);
    }
}
