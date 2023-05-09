using CheckflixApp.Application.Common.Security;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Followings.Commands.UnfollowUser;

[Authorize]
public record UnfollowUserCommand(string UserId) : IRequest<Result<string>>;