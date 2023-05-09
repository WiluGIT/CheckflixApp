using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Identity.Users.Queries.GetUserRoles;

public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, Result<List<UserRoleDto>>>
{
    private readonly IUserService _userService;

    public GetUserRolesQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result<List<UserRoleDto>>> Handle(GetUserRolesQuery query, CancellationToken cancellationToken)
    {
        return await _userService.GetRolesAsync(query.Id, cancellationToken);
    }
}
