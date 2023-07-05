using CheckflixApp.Application.ApplicationUserProductions.Commands.CreateUserProductionCommand;
using CheckflixApp.Application.ApplicationUserProductions.Commands.UpdateUserProductionCommand;
using CheckflixApp.Application.ApplicationUserProductions.Common;
using CheckflixApp.Application.ApplicationUserProductions.Queries.GetUserProductionsQuery;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebUI.Controllers;

public class ApplicationUserProductionsController : ApiControllerBase
{
    [HttpGet]
    [OpenApiOperation("Get collection of user productions.", "")]
    public async Task<IActionResult> GetUserProductions([FromQuery] GetUserProductionsQuery query) =>
        await Result.From(query)
        .Bind(query => Mediator.Send(query))
        .Match(response => Ok(response), errors => Problem(errors));

    [HttpPost]
    [OpenApiOperation("Create application user production.", "")]
    public async Task<IActionResult> CreateUserProduction([FromBody] CreateUserProductionCommand command) =>
        await Result.From(command)
        .Bind(command => Mediator.Send(command))
        .Match(response => Ok(response), errors => Problem(errors));

    [HttpPut("{id}")]
    [OpenApiOperation("Update application user production.", "")]
    public async Task<IActionResult> UpdateUserProduction([FromRoute] int id, [FromBody] UpdateUserProductionRequest request) =>
        await Result.From(new UpdateUserProductionCommand(id, request))
        .Bind(command => Mediator.Send(command))
        .Match(response => Ok(response), errors => Problem(errors));
}
