using System.Security.Claims;
using CheckflixApp.Application.Identity.Tokens.Queries.GetDiscordToken;
using CheckflixApp.Application.Identity.Tokens.Queries.GetRefreshToken;
using CheckflixApp.Application.Identity.Tokens.Queries.GetToken;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.Infrastructure.Identity;
using CheckflixApp.WebUI.Controllers;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using static CheckflixApp.Domain.Common.Errors.DomainErrors;

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


    [HttpGet("discord-callback")]
    public async Task<IActionResult> DiscordCallback([FromQuery] GetDiscordTokenQuery query) =>
        await Result.From(query)
        .Bind(query => Mediator.Send(query))
        .Match(response => Ok(response), errors => Problem(errors));
}
