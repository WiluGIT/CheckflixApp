using CheckflixApp.Application.Followings.Commands.FollowUser;
using CheckflixApp.Application.Followings.Commands.UnfollowUser;
using CheckflixApp.Application.Followings.Common;
using CheckflixApp.Application.Followings.Queries.GetUserFollowingsCount;
using CheckflixApp.Application.Identity.Roles.Queries.GetRoleById;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using static CheckflixApp.Domain.Common.Errors.DomainErrors;

namespace WebUI.Controllers;

public class FollowingsController : ApiControllerBase
{
    [HttpPost("{userId}/follow")]
    [OpenApiOperation("Follow user with given userId.", "")]
    public async Task<IActionResult> FollowUser([FromRoute] string userId) =>
        await Result.From(new FollowUserCommand(userId))
        .Bind(query => Mediator.Send(query))
        .Match(response => Ok(response), errors => Problem(errors));

    [HttpDelete("{userId}/follow")]
    [OpenApiOperation("Unfollow user with given userId.", "")]
    public async Task<IActionResult> UnfollowUser(string userId) =>
        await Result.From(new UnfollowUserCommand(userId))
        .Bind(query => Mediator.Send(query))
        .Match(response => Ok(response), errors => Problem(errors));

    [HttpGet("follow-count")]
    [OpenApiOperation("Get logged in user followings count.", "")]
    public async Task<IActionResult> GetUserFollowings() =>
        await Result.From(new GetFollowingsCountQuery())
        .Bind(query => Mediator.Send(query))
        .Match(response => Ok(response), errors => Problem(errors));
}