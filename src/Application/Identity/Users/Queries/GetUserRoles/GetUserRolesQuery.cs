using CheckflixApp.Application.Common.Security;
using CheckflixApp.Application.Identity.Common;
using MediatR;

namespace CheckflixApp.Application.Identity.Users.Queries.GetUserRoles;

[Authorize(Roles = "Administrator")]
public record GetUserRolesQuery(string Id) : IRequest<List<UserRoleDto>>;