using CheckflixApp.Application.Common.Security;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Followings.Commands.FollowUser;

[Authorize]
public record FollowUserCommand(string UserId) : IRequest<Result<string>>;