using CheckflixApp.Application.Followings.Commands.FollowUser;
using CheckflixApp.Application.Followings.Commands.UnfollowUser;
using CheckflixApp.Application.Followings.Common;
using CheckflixApp.Application.Followings.Queries.GetUserFollowingsCount;
using CheckflixApp.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebUI.Controllers;

public class FollowingsController : ApiControllerBase
{
    [HttpPost("{userId}/follow")]
    [OpenApiOperation("Follow user with given userId.", "")]
    public async Task<string> FollowUser([FromRoute] string userId)
        => await Mediator.Send(new FollowUserCommand(userId));

    [HttpDelete("{userId}/follow")]
    [OpenApiOperation("Unfollow user with given userId.", "")]
    public async Task<string> UnfollowUser(string userId)
        => await Mediator.Send(new UnfollowUserCommand(userId));

    [HttpGet("follow-count")]
    [OpenApiOperation("Get logged in user followings count.", "")]
    public async Task<UserFollowingsCountDto> GetUserFollowings()
        => await Mediator.Send(new GetFollowingsCountQuery());
}