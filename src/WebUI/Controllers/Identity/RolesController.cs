using CheckflixApp.Application.Identity.Common;
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
}
