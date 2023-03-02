using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Application.Identity.Users.Commands.AssignRolesCommand;
using CheckflixApp.Application.Identity.Users.Commands.CreateUser;
using CheckflixApp.Application.Identity.Users.Commands.ToggleUserStatus;
using CheckflixApp.Application.Identity.Users.Queries.GetById;
using CheckflixApp.Application.Identity.Users.Queries.GetList;
using CheckflixApp.Application.Identity.Users.Queries.GetUserRoles;
using CheckflixApp.WebUI.Controllers;
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
    public async Task<string> CreateAsync(CreateUserCommand command)
        => await Mediator.Send(command);

    [HttpGet("{id}/roles")]
    [OpenApiOperation("Get a user's roles.", "")]
    public async Task<List<UserRoleDto>> GetRolesAsync(string id, CancellationToken cancellationToken)
        => await Mediator.Send(new GetUserRolesQuery(id));

    [HttpPost("{id}/roles")]
    [OpenApiOperation("Update a user's assigned roles.", "")]
    public async Task<string> AssignRolesAsync(string id, UserRolesRequest request, CancellationToken cancellationToken)
        => await Mediator.Send(new AssignRolesCommand(id, request));

    [HttpPut("{id}/toggle-status")]
    [OpenApiOperation("Toggle a user's active status.", "")]
    public async Task ToggleStatusAsync(ToggleUserStatusCommand command)
        => await Mediator.Send(command);
}
