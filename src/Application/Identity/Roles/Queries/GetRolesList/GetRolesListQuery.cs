using CheckflixApp.Application.Common.Security;
using CheckflixApp.Application.Identity.Common;
using MediatR;

namespace CheckflixApp.Application.Identity.Roles.Queries.GetRolesList;

[Authorize(Roles = "Administrator")]
public record GetRolesListQuery() : IRequest<List<RoleDto>>;