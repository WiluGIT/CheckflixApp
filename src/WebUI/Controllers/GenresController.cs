using CheckflixApp.Application.Common.Models;
using CheckflixApp.Application.Genres.Common;
using CheckflixApp.Application.Genres.Queries.GetGenresProductionsQuery;
using CheckflixApp.Application.Genres.Queries.GetGenresQuery;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebUI.Controllers;

public class GenresController : ApiControllerBase
{
    [HttpGet]
    [OpenApiOperation("Get all production genres.", "")]
    public async Task<IActionResult> GetGenresAsync() =>
        await Result.From(new GetGenresQuery())
        .Bind(query => Mediator.Send(query))
        .Match(response => Ok(response), errors => Problem(errors));


    [HttpGet("productions")]
    [OpenApiOperation("Get all production genres.", "")]
    public async Task<IActionResult> GetGenreProductionsAsync([FromQuery] GenresFilter filter) =>
        await Result.From(new GetGenresProductionsQuery(filter))
        .Bind(query => Mediator.Send(query))
        .Match(response => Ok(response), errors => Problem(errors));
}
