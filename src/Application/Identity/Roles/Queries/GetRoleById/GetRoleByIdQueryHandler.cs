using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Identity.Roles.Queries.GetRoleById;

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Result<RoleDto>>
{
    private readonly IRoleService _roleService;

    public GetRoleByIdQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<Result<RoleDto>> Handle(GetRoleByIdQuery query, CancellationToken cancellationToken)
    {
        return await _roleService.GetByIdAsync(query.Id);
    }
}
