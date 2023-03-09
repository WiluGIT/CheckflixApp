using CheckflixApp.Application.Common.Security;
using MediatR;

namespace CheckflixApp.Application.Followings.Commands.UnfollowUser;

[Authorize]
public record UnfollowUserCommand(string UserId) : IRequest<string>;