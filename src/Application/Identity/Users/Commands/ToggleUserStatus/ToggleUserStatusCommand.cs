using CheckflixApp.Application.Common.Security;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Identity.Users.Commands.ToggleUserStatus;

[Authorize(Roles = "Administrator")]
public record ToggleUserStatusCommand(string Id, bool ActivateUser) : IRequest<Result<Unit>>;