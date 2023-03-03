using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Tokens.Queries.GetRefreshToken;
using CheckflixApp.Application.Identity.Tokens.Queries.GetToken;
using CheckflixApp.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebUI.Controllers.Identity;

public class TokensController : ApiControllerBase
{
    [HttpPost]
    [OpenApiOperation("Request an access token using credentials.", "")]
    public async Task<TokenDto> GetTokenAsync(GetTokenQuery query)
        => await Mediator.Send(query);

    [HttpPost("refresh")]
    [OpenApiOperation("Request an access token using a refresh token.", "")]
    public async Task<TokenDto> GetRefreshTokenAsync(GetRefreshTokenQuery query)
        => await Mediator.Send(query);
}
