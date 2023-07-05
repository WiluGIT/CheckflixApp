using CheckflixApp.Application.Followings.Commands.FollowUser;
using CheckflixApp.Application.Followings.Commands.UnfollowUser;
using CheckflixApp.Application.Followings.Queries.GetFollowersQuery;
using CheckflixApp.Application.Followings.Queries.GetFollowingQuery;
using CheckflixApp.Application.Followings.Queries.GetUserFollowingsCount;
using CheckflixApp.Application.Followings.Queries.SearchUsers;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebUI.Controllers;

public class FollowingsController : ApiControllerBase
{
    [HttpPost("{userId}")]
    [OpenApiOperation("Follow user with given userId.", "")]
    public async Task<IActionResult> FollowUser([FromRoute] string userId) =>
        await Result.From(new FollowUserCommand(userId))
        .Bind(query => Mediator.Send(query))
        .Match(response => Ok(response), errors => Problem(errors));

    [HttpDelete("{userId}")]
    [OpenApiOperation("Unfollow user with given userId.", "")]
    public async Task<IActionResult> UnfollowUser(string userId) =>
        await Result.From(new UnfollowUserCommand(userId))
        .Bind(query => Mediator.Send(query))
        .Match(response => Ok(response), errors => Problem(errors));

    [HttpGet("follow-count")]
    [OpenApiOperation("Get logged in user followings count.", "")]
    public async Task<IActionResult> GetUserFollowingsCount([FromQuery] GetFollowingsCountQuery query) =>
        await Result.From(query)
        .Bind(query => Mediator.Send(query))
        .Match(response => Ok(response), errors => Problem(errors));

    [HttpGet("users")]
    [OpenApiOperation("Get logged in user followings count.", "")]
    public async Task<IActionResult> UsersWithFollowing([FromQuery] SearchUsersQuery query) =>
        await Result.From(query)
        .Bind(query => Mediator.Send(query))
        .Match(response => Ok(response), errors => Problem(errors));

    [HttpGet("followers")]
    [OpenApiOperation("Get logged in user followings count.", "")]
    public async Task<IActionResult> GetUserFollowers([FromQuery] GetFollowersQuery query) =>
    await Result.From(query)
    .Bind(query => Mediator.Send(query))
    .Match(response => Ok(response), errors => Problem(errors));

    [HttpGet("following")]
    [OpenApiOperation("Get logged in user followings count.", "")]
    public async Task<IActionResult> GetUserFollowing([FromQuery] GetFollowingQuery query) =>
        await Result.From(query)
        .Bind(query => Mediator.Send(query))
        .Match(response => Ok(response), errors => Problem(errors));
}