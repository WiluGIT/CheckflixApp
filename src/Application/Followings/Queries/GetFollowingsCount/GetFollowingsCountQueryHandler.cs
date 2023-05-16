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
    private readonly ICurrentUserService _currentUserService;
    private readonly IStringLocalizer<GetFollowingsCountQueryHandler> _localizer;

    public GetFollowingsCountQueryHandler(IUserService userService, ICurrentUserService currentUserService, IStringLocalizer<GetFollowingsCountQueryHandler> localizer)
    {
        _userService = userService;
        _currentUserService = currentUserService;
        _localizer = localizer;
    }

    public async Task<Result<UserFollowingsCountDto>> Handle(GetFollowingsCountQuery query, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId ?? string.Empty;

        var followingCount = await _userService.GetFollowingCountAsync(userId, cancellationToken);

        if (followingCount == null)
        {
            return Error.NotFound(description: _localizer["User Followings Not Found"]);
        }

        return followingCount;
    }
}
