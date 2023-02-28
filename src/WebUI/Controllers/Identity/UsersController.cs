using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Users.Commands.CreateUser;
using CheckflixApp.Application.Identity.Users.Commands.ToggleUserStatus;
using CheckflixApp.Application.Identity.Users.Queries.GetById;
using CheckflixApp.Application.Identity.Users.Queries.GetList;
using CheckflixApp.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebUI.Controllers.Identity;

public class UsersController : ApiControllerBase
{
    // Add endpoint authorization
    [HttpGet]
    [OpenApiOperation("Get list of all users.", "")]
    public async Task<List<UserDetailsDto>> GetListAsync()
        => await Mediator.Send(new GetListQuery());

    // Check if its properly binded from query
    [HttpGet("{id}")]
    [OpenApiOperation("Get a user's details.", "")]
    public async Task<UserDetailsDto> GetByIdAsync([FromRoute] string id)
        => await Mediator.Send(new GetByIdQuery(id));

    [HttpPost]
    [OpenApiOperation("Creates a new user.", "")]
    public async Task<string> CreateAsync(CreateUserCommand command)
        => await Mediator.Send(command);

    [HttpPut("{id}/toggle-status")]
    [OpenApiOperation("Toggle a user's active status.", "")]
    public async Task ToggleStatusAsync(ToggleUserStatusCommand command)
        => await Mediator.Send(command);
}
