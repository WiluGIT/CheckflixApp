using CheckflixApp.Application.Identity.Common;
using MediatR;

namespace CheckflixApp.Application.Identity.Users.Commands.AssignRoles;
public record AssignRolesCommand(string Id, UserRolesRequest UserRequest) : IRequest<string>;