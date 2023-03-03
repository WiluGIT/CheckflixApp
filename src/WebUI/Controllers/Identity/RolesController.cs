using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Application.Identity.Roles.Commands.CreateOrUpdateRole;
using CheckflixApp.Application.Identity.Roles.Commands.DeleteRole;
using CheckflixApp.Application.Identity.Roles.Queries.GetRoleById;
using CheckflixApp.Application.Identity.Roles.Queries.GetRolesList;
using CheckflixApp.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebUI.Controllers.Identity;

public class RolesController : ApiControllerBase
{
    [HttpGet]
    [OpenApiOperation("Get a list of all roles.", "")]
    public async Task<List<RoleDto>> GetListAsync(CancellationToken cancellationToken)
        => await Mediator.Send(new GetRolesListQuery());

    [HttpGet("{id}")]
    [OpenApiOperation("Get role details.", "")]
    public async Task<RoleDto> GetByIdAsync([FromRoute] string id)
        => await Mediator.Send(new GetRoleByIdQuery(id));

    [HttpPost]
    [OpenApiOperation("Create or update a role.", "")]
    public async Task<string> RegisterRoleAsync(CreateOrUpdateRoleCommand command)
        => await Mediator.Send(command);

    [HttpDelete("{id}")]
    [OpenApiOperation("Delete a role.", "")]
    public async Task<string> DeleteAsync([FromRoute] string id)
        => await Mediator.Send(new DeleteRoleCommand(id));
}
