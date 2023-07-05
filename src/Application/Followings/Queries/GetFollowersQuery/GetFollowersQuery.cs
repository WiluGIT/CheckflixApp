using CheckflixApp.Application.Common.Security;
using CheckflixApp.Application.Followings.Common;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Followings.Queries.GetFollowersQuery;

[Authorize]
public record GetFollowersQuery(string UserId) : IRequest<Result<List<UserWithFollowingDto>>>;