using CheckflixApp.Application.Auditing.Common;
using CheckflixApp.Application.Auditing.Queries;
using CheckflixApp.Application.Common.Models;
using CheckflixApp.Application.Identity.Personal.Commands.ChangePassword;
using CheckflixApp.Application.Identity.Personal.Commands.UpdateUser;
using CheckflixApp.Application.Identity.Personal.Queries.GetProfile;
using CheckflixApp.Application.Productions.Commands.CreateProductionCommand;
using CheckflixApp.Application.Productions.Commands.DeleteProductionCommand;
using CheckflixApp.Application.Productions.Queries.GetProductionByIdQuery;
using CheckflixApp.Application.Productions.Queries.GetProductionsQuery;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebUI.Controllers;

public class ProductionsController : ApiControllerBase
{
    [HttpGet("{id}")]
    [OpenApiOperation("Get production with specified id.", "")]
    public async Task<IActionResult> GetById(int id) =>
        await Result.From(new GetProductionByIdQuery(id))
        .Bind(query => Mediator.Send(query))
        .Match(response => Ok(response), errors => Problem(errors));

    [HttpGet]
    [OpenApiOperation("Get paginated collection of productions.", "")]
    public async Task<IActionResult> GetProductions([FromQuery] PaginationFilter filter) =>
        await Result.From(new GetProductionsQuery(filter))
        .Bind(command => Mediator.Send(command))
        .Match(response => Ok(response), errors => Problem(errors));

    [HttpPost]
    [OpenApiOperation("Create production.", "")]
    public async Task<IActionResult> CreateProduction([FromBody] CreateProductionCommand command) =>
        await Result.From(command)
        .Bind(command => Mediator.Send(command))
        .Match(response => Created(nameof(GetById), response), errors => Problem(errors));

    [HttpDelete("{id}")]
    [OpenApiOperation("Delete production.", "")]
    public async Task<IActionResult> DeleteProduction(int id) =>
        await Result.From(new DeleteProductionCommand(id))
        .Bind(command => Mediator.Send(command))
        .Match(response => Created(nameof(GetById), response), errors => Problem(errors));
}
