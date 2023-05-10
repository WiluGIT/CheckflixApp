using CheckflixApp.Application.Identity.Roles.Commands.CreateOrUpdateRole;
using CheckflixApp.Application.Identity.Roles.Commands.DeleteRole;
using CheckflixApp.Application.Identity.Roles.Queries.GetRoleById;
using CheckflixApp.Application.Identity.Roles.Queries.GetRolesList;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebUI.Controllers;

public class RolesController : ApiControllerBase
{
    [HttpGet]
    [OpenApiOperation("Get a list of all roles.", "")]
    public async Task<IActionResult> GetListAsync(CancellationToken cancellationToken) =>
        await Result.From(new GetRolesListQuery())
        .Bind(query => Mediator.Send(query))
        .Match(response => Ok(response), errors => Problem(errors));

    [HttpGet("{id}")]
    [OpenApiOperation("Get role details.", "")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id) =>
        await Result.From(new GetRoleByIdQuery(id))
        .Bind(query => Mediator.Send(query))
        .Match(response => Ok(response), errors => Problem(errors));

    [HttpPost]
    [OpenApiOperation("Create or update a role.", "")]
    public async Task<IActionResult> RegisterRoleAsync(CreateOrUpdateRoleCommand command) =>
        await Result.From(command)
        .Bind(query => Mediator.Send(query))
        .Match(response => Ok(response), errors => Problem(errors));

    [HttpDelete("{id}")]
    [OpenApiOperation("Delete a role.", "")]
    public async Task<IActionResult> DeleteAsync([FromRoute] string id) =>
        await Result.From(new DeleteRoleCommand(id))
        .Bind(query => Mediator.Send(query))
        .Match(response => Ok(response), errors => Problem(errors));
}
