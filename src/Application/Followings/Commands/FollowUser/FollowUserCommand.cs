using CheckflixApp.Application.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CheckflixApp.Application.Followings.Commands.FollowUser;

[Authorize]
public record FollowUserCommand(string UserId) : IRequest<string>;