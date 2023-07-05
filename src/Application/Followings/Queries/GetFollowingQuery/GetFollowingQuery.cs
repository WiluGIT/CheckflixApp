using CheckflixApp.Application.Common.Security;
using CheckflixApp.Application.Followings.Common;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Followings.Queries.GetFollowingQuery;

[Authorize]
public record GetFollowingQuery(string UserId) : IRequest<Result<List<UserWithFollowingDto>>>;