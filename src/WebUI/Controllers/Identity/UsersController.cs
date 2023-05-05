using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
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
using CheckflixApp.Domain.Common.Errors;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebUI.Controllers.Identity;

public class UsersController : ApiControllerBase
{
    [HttpGet]
    [OpenApiOperation("Get list of all users.", "")]
    public async Task<List<UserDetailsDto>> GetListAsync()
        => await Mediator.Send(new GetListQuery());

    [HttpGet("{id}")]
    [OpenApiOperation("Get a user's details.", "")]
    public async Task<UserDetailsDto> GetByIdAsync([FromRoute] string id)
        => await Mediator.Send(new GetByIdQuery(id));

    [HttpPost]
    [OpenApiOperation("Creates a new user.", "")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> CreateAsync(CreateUserCommand command) =>
    //    await Result.Create(command, DomainErrors.General.UnProcessableRequest)
    //        .Bind(command => Mediator.Send(command))
    //        .Match(Ok, BadRequest);
    public async Task<IActionResult> CreateAsync(CreateUserCommand command)
    {
        var res = await Mediator.Send(command);


        return BadRequest(res.ToResult());
    }

    [HttpGet("{id}/roles")]
    [OpenApiOperation("Get a user's roles.", "")]
    public async Task<List<UserRoleDto>> GetRolesAsync([FromRoute] string id)
        => await Mediator.Send(new GetUserRolesQuery(id));

    [HttpPost("{id}/roles")]
    [OpenApiOperation("Update a user's assigned roles.", "")]
    public async Task<string> AssignRolesAsync([FromRoute] string id, [FromBody] UserRolesRequest request)
        => await Mediator.Send(new AssignRolesCommand(id, request));

    [HttpPut("{id}/toggle-status")]
    [OpenApiOperation("Toggle a user's active status.", "")]
    public async Task ToggleStatusAsync([FromRoute] string id, [FromBody] bool activateUser)
        => await Mediator.Send(new ToggleUserStatusCommand(id, activateUser));

    [HttpGet("confirm-email")]
    [OpenApiOperation("Confirm email address for a user.", "")]
    public async Task<string> ConfirmEmailAsync([FromQuery] ConfirmEmailCommand command)
        => await Mediator.Send(command);

    [HttpGet("confirm-phone-number")]
    [OpenApiOperation("Confirm phone number for a user.", "")]
    public async Task<string> ConfirmPhoneNumberAsync([FromQuery] ConfirmPhoneNumerCommand command)
        => await Mediator.Send(command);

    [HttpPost("forgot-password")]
    [OpenApiOperation("Request a password reset email for a user.", "")]
    public async Task<string> ForgotPasswordAsync(ForgotPasswordCommand command)
        => await Mediator.Send(command);

    [HttpPost("reset-password")]
    [OpenApiOperation("Reset a user's password.", "")]
    public async Task<string> ResetPasswordAsync(ResetPasswordCommand command)
        => await Mediator.Send(command);
}
