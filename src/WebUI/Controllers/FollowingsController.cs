using CheckflixApp.Application.Auditing.Common;
using CheckflixApp.Application.Auditing.Queries;
using CheckflixApp.Application.Followings.Commands.FollowUser;
using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Personal.Commands.ChangePassword;
using CheckflixApp.Application.Identity.Personal.Commands.UpdateUser;
using CheckflixApp.Application.Identity.Personal.Queries.GetProfile;
using CheckflixApp.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebUI.Controllers;


public class FollowingsController : ApiControllerBase
{
    [HttpPost("{userId}/follow")]
    public async Task<string> FollowUser([FromRoute] string userId)
        => await Mediator.Send(new FollowUserCommand(userId));

//    [HttpDelete("{userId}/follow")]
//    public Task<string> UnfollowUser(string userId, CancellationToken cancellationToken)
//        => await Mediator.Send(new Delete.Command(username), cancellationToken);
}