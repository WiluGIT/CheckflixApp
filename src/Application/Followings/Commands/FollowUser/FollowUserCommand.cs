using CheckflixApp.Application.Common.Security;
using MediatR;

namespace CheckflixApp.Application.Followings.Commands.FollowUser;

[Authorize]
public record FollowUserCommand(string UserId) : IRequest<string>;