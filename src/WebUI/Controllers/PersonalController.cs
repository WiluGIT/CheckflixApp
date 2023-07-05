using CheckflixApp.Application.Auditing.Common;
using CheckflixApp.Application.Auditing.Queries;
using CheckflixApp.Application.Identity.Personal.Commands.ChangePassword;
using CheckflixApp.Application.Identity.Personal.Commands.UpdateUser;
using CheckflixApp.Application.Identity.Personal.Queries.GetProfile;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebUI.Controllers;

public class PersonalController : ApiControllerBase
{
    [HttpGet("profile")]
    [OpenApiOperation("Get profile details of currently logged in user.", "")]
    public async Task<IActionResult> GetProfileAsync([FromQuery] GetProfileQuery query, CancellationToken cancellationToken) =>
        await Result.From(query)
        .Bind(query => Mediator.Send(query))
        .Match(response => Ok(response), errors => Problem(errors));

    [HttpPut("profile")]
    [OpenApiOperation("Update profile details of currently logged in user.", "")]
    public async Task<IActionResult> UpdateProfileAsync([FromForm] UpdateUserCommand command) =>
        await Result.From(command)
        .Bind(command => Mediator.Send(command))
        .Match(response => Ok(response), errors => Problem(errors));

    [HttpPut("change-password")]
    [OpenApiOperation("Change password of currently logged in user.", "")]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordCommand command) => 
        await Result.From(command)
        .Bind(command => Mediator.Send(command))
        .Match(response => Ok(response), errors => Problem(errors));

    // TODO: Audit logs
    [HttpGet("logs")]
    [OpenApiOperation("Get audit logs of currently logged in user.", "")]
    public async Task<List<AuditDto>> GetLogsAsync()
        => await Mediator.Send(new GetMyAuditLogsQuery());
}
