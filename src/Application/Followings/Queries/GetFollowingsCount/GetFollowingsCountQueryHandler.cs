using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Followings.Common;
using CheckflixApp.Application.Followings.Queries.GetUserFollowingsCount;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Domain.Common.Primitives;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.Followings.Queries.GetFollowingsCount;

public class GetFollowingsCountQueryHandler : IRequestHandler<GetFollowingsCountQuery, Result<UserFollowingsCountDto>>
{
    private readonly IUserService _userService;
    private readonly IStringLocalizer<GetFollowingsCountQueryHandler> _localizer;

    public GetFollowingsCountQueryHandler(IUserService userService, IStringLocalizer<GetFollowingsCountQueryHandler> localizer)
    {
        _userService = userService;
        _localizer = localizer;
    }

    public async Task<Result<UserFollowingsCountDto>> Handle(GetFollowingsCountQuery query, CancellationToken cancellationToken)
    {
        var followingCount = await _userService.GetFollowingCountAsync(query.UserId, cancellationToken);

        if (followingCount == null)
        {
            return Error.NotFound(description: _localizer["User Followings Not Found"]);
        }

        return followingCount;
    }
}
