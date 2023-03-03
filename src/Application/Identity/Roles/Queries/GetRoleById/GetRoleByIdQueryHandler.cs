using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using MediatR;

namespace CheckflixApp.Application.Identity.Roles.Queries.GetRoleById;

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto>
{
    private readonly IRoleService _roleService;

    public GetRoleByIdQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<RoleDto> Handle(GetRoleByIdQuery query, CancellationToken cancellationToken)
    {
        return await _roleService.GetByIdAsync(query.Id);
    }
}
