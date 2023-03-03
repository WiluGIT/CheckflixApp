using CheckflixApp.Application.Common.Security;
using CheckflixApp.Application.Identity.Common;
using MediatR;

namespace CheckflixApp.Application.Identity.Users.Commands.AssignRoles;

[Authorize(Roles = "Administrator")]
public record AssignRolesCommand(string Id, UserRolesRequest UserRequest) : IRequest<string>;