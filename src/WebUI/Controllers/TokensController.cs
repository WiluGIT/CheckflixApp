using CheckflixApp.Application.Identity.Tokens.Queries.GetRefreshToken;
using CheckflixApp.Application.Identity.Tokens.Queries.GetToken;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebUI.Controllers;

public class TokensController : ApiControllerBase
{
    [HttpPost]
    [OpenApiOperation("Request an access token using credentials.", "")]
    public async Task<IActionResult> GetTokenAsync(GetTokenQuery query) =>
        await Result.From(query)
        .Bind(query => Mediator.Send(query))
        .Match(response => Ok(response), errors => Problem(errors));

    [HttpPost("refresh")]
    [OpenApiOperation("Request an access token using a refresh token.", "")]
    public async Task<IActionResult> GetRefreshTokenAsync(GetRefreshTokenQuery query) =>
        await Result.From(query)
        .Bind(query => Mediator.Send(query))
        .Match(response => Ok(response), errors => Problem(errors));
}
