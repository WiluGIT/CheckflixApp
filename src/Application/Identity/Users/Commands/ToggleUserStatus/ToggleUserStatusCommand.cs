using CheckflixApp.Application.Common.Security;
using MediatR;

namespace CheckflixApp.Application.Identity.Users.Commands.ToggleUserStatus;

[Authorize(Roles = "Administrator")]
public record ToggleUserStatusCommand(string Id, bool ActivateUser) : IRequest<Unit>;