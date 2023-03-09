using CheckflixApp.Application.Common.Security;
using CheckflixApp.Application.Followings.Common;
using CheckflixApp.Application.Identity.Common;
using MediatR;

namespace CheckflixApp.Application.Followings.Queries.GetUserFollowingsCount;

[Authorize]
public record GetFollowingsCountQuery() : IRequest<UserFollowingsCountDto>;