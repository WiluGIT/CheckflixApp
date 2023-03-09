using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Followings.Common;
using CheckflixApp.Application.Followings.Queries.GetUserFollowingsCount;
using CheckflixApp.Application.Identity.Interfaces;
using MediatR;

namespace CheckflixApp.Application.Followings.Queries.GetFollowingsCount;

public class GetFollowingsCountQueryHandler : IRequestHandler<GetFollowingsCountQuery, UserFollowingsCountDto>
{
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;

    public GetFollowingsCountQueryHandler(IUserService userService, ICurrentUserService currentUserService)
    {
        _userService = userService;
        _currentUserService = currentUserService;
    }

    public async Task<UserFollowingsCountDto> Handle(GetFollowingsCountQuery query, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId ?? string.Empty;

        return await _userService.GetFollowingCountAsync(userId, cancellationToken);
    }
}
