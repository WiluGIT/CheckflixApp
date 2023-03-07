using CheckflixApp.Application.Auditing.Common;
using CheckflixApp.Application.Auditing.Queries;
using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Application.Identity.Personal.Commands.ChangePassword;
using CheckflixApp.Application.Identity.Personal.Commands.UpdateUser;
using CheckflixApp.Application.Identity.Personal.Queries.GetProfile;
using CheckflixApp.Infrastructure.Extensions;
using CheckflixApp.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebUI.Controllers.Personal;

public class PersonalController : ApiControllerBase
{
    [HttpGet("profile")]
    [OpenApiOperation("Get profile details of currently logged in user.", "")]
    public async Task<ActionResult<UserDetailsDto>> GetProfileAsync(CancellationToken cancellationToken)
        => await Mediator.Send(new GetProfileQuery());

    [HttpPut("profile")]
    [OpenApiOperation("Update profile details of currently logged in user.", "")]
    public async Task<ActionResult<string>> UpdateProfileAsync([FromForm]UpdateUserCommand command)
        => await Mediator.Send(command);

    [HttpPut("change-password")]
    [OpenApiOperation("Change password of currently logged in user.", "")]
    public async Task<ActionResult<string>> ChangePasswordAsync(ChangePasswordCommand command)
        => await Mediator.Send(command);

    // TODO: Audit logs
    [HttpGet("logs")]
    [OpenApiOperation("Get audit logs of currently logged in user.", "")]
    public async Task<List<AuditDto>> GetLogsAsync()
        => await Mediator.Send(new GetMyAuditLogsQuery());
}
