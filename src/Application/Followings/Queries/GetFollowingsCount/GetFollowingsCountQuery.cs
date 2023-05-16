using CheckflixApp.Application.Common.Security;
using CheckflixApp.Application.Followings.Common;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Followings.Queries.GetUserFollowingsCount;

[Authorize]
public record GetFollowingsCountQuery() : IRequest<Result<UserFollowingsCountDto>>;