using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Users.Commands.AssignRoles;
using CheckflixApp.Application.Identity.Users.Commands.ConfirmEmail;
using CheckflixApp.Application.Identity.Users.Commands.ConfirmPhoneNumber;
using CheckflixApp.Application.Identity.Users.Commands.CreateUser;
using CheckflixApp.Application.Identity.Users.Commands.ForgotPassword;
using CheckflixApp.Application.Identity.Users.Commands.ResetPassword;
using CheckflixApp.Application.Identity.Users.Commands.ToggleUserStatus;
using CheckflixApp.Application.Identity.Users.Queries.GetById;
using CheckflixApp.Application.Identity.Users.Queries.GetList;
using CheckflixApp.Application.Identity.Users.Queries.GetUserRoles;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebUI.Controllers;

public class UsersController : ApiControllerBase
{
    [HttpGet]
    [OpenApiOperation("Get list of all users.", "")]
    public async Task<IActionResult> GetListAsync() =>
        await Result.From(new GetListQuery())
        .Bind(command => Mediator.Send(command))
        .Match(response => Ok(response), errors => Problem(errors));

    [HttpGet("{id}")]
    [OpenApiOperation("Get a user's details.", "")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id) =>
        await Result.From(new GetByIdQuery(id))
        .Bind(command => Mediator.Send(command))
        .Match(response => Ok(response), errors => Problem(errors));

    [HttpPost]
    [OpenApiOperation("Creates a new user.", "")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<ProblemDetails>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync(CreateUserCommand command) =>
        await Result.From(command)
        .Bind(command => Mediator.Send(command))
        .Match(response => CreatedAtAction(nameof(GetByIdAsync), new { id = response.Item2 }, response.Item1), errors => Problem(errors));

    [HttpGet("{id}/roles")]
    [OpenApiOperation("Get a user's roles.", "")]
    public async Task<IActionResult> GetRolesAsync([FromRoute] string id) =>
        await Result.From(new GetUserRolesQuery(id))
        .Bind(command => Mediator.Send(command))
        .Match(response => Ok(response), errors => Problem(errors));

    [HttpPost("{id}/roles")]
    [OpenApiOperation("Update a user's assigned roles.", "")]
    public async Task<IActionResult> AssignRolesAsync([FromRoute] string id, [FromBody] UserRolesRequest request) =>
        await Result.From(new AssignRolesCommand(id, request))
        .Bind(command => Mediator.Send(command))
        .Match(response => Ok(response), errors => Problem(errors));

    [HttpPut("{id}/toggle-status")]
    [OpenApiOperation("Toggle a user's active status.", "")]
    public async Task<IActionResult> ToggleStatusAsync([FromRoute] string id, [FromBody] bool activateUser) =>
        await Result.From(new ToggleUserStatusCommand(id, activateUser))
        .Bind(command => Mediator.Send(command))
        .Match(response => NoContent(), errors => Problem(errors));

    [HttpGet("confirm-email")]
    [OpenApiOperation("Confirm email address for a user.", "")]
    public async Task<IActionResult> ConfirmEmailAsync([FromQuery] ConfirmEmailCommand command) =>
        await Result.From(command)
        .Bind(command => Mediator.Send(command))
        .Match(response => Ok(response), errors => Problem(errors));

    [HttpGet("confirm-phone-number")]
    [OpenApiOperation("Confirm phone number for a user.", "")]
    public async Task<IActionResult> ConfirmPhoneNumberAsync([FromQuery] ConfirmPhoneNumerCommand command) =>
        await Result.From(command)
        .Bind(command => Mediator.Send(command))
        .Match(response => Ok(response), errors => Problem(errors));

    [HttpPost("forgot-password")]
    [OpenApiOperation("Request a password reset email for a user.", "")]
    public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordCommand command) =>
        await Result.From(command)
        .Bind(command => Mediator.Send(command))
        .Match(response => Ok(response), errors => Problem(errors));

    [HttpPost("reset-password")]
    [OpenApiOperation("Reset a user's password.", "")]
    public async Task<IActionResult> ResetPasswordAsync(ResetPasswordCommand command) =>
        await Result.From(command)
        .Bind(command => Mediator.Send(command))
        .Match(response => Ok(response), errors => Problem(errors));
}
