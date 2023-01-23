using CheckflixApp.Application.Identity.Dtos;
using CheckflixApp.Application.Identity.Tokens.Queries.GetRefreshToken;
using CheckflixApp.Application.Identity.Tokens.Queries.GetToken;
using CheckflixApp.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebUI.Controllers.Identity;

public class TokensController : ApiControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    [OpenApiOperation("Request an access token using credentials.", "")]
    public async Task<TokenDto> GetTokenAsync(GetTokenQuery query)
    => await Mediator.Send(query);

    [HttpPost("refresh")]
    [AllowAnonymous]
    [OpenApiOperation("Request an access token using a refresh token.", "")]
    public async Task<TokenDto> GetRefreshTokenAsync(GetRefreshTokenQuery query)
        => await Mediator.Send(query);
}
