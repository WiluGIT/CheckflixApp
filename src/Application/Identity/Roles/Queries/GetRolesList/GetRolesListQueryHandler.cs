﻿using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Application.Identity.Users.Queries.GetUserRoles;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Identity.Roles.Queries.GetRolesList;

public class GetRolesListQueryHandler : IRequestHandler<GetRolesListQuery, Result<List<RoleDto>>>
{
    private readonly IRoleService _roleService;

    public GetRolesListQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<Result<List<RoleDto>>> Handle(GetRolesListQuery query, CancellationToken cancellationToken)
    {
        return await _roleService.GetListAsync(cancellationToken);
    }
}
