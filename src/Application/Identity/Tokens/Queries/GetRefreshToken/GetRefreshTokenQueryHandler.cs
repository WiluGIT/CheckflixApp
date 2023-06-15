using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Domain.Common.Consts;
using CheckflixApp.Domain.Common.Primitives;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.Identity.Tokens.Queries.GetRefreshToken;

public class GetRefreshTokenQueryHandler : IRequestHandler<GetRefreshTokenQuery, Result<AccessTokenDto>>
{
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStringLocalizer<GetRefreshTokenQueryHandler> _localizer;

    public GetRefreshTokenQueryHandler(ITokenService tokenService, IHttpContextAccessor httpContextAccessor, IStringLocalizer<GetRefreshTokenQueryHandler> localizer)
    {
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
        _localizer = localizer;
    }

    public async Task<Result<AccessTokenDto>> Handle(GetRefreshTokenQuery query, CancellationToken cancellationToken)
    {
        var ipAddress = _httpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For")
            ? _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"].ToString()
            : _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "N/A";

        var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies[AuthKeys.RefreshTokenKey];
        if (refreshToken is null)
        {
            return Error.Unauthorized(description: _localizer["identity.invalidrefreshtoken"]);
        }

        var tokenResult = await _tokenService.GetRefreshTokenAsync(query.Token, refreshToken, ipAddress, cancellationToken);

        if (tokenResult.IsFailure)
        {
            return tokenResult.Errors;
        }

        _httpContextAccessor.HttpContext.Response.Cookies.Append(
            AuthKeys.RefreshTokenKey, 
            tokenResult.Value.RefreshToken,
            new CookieOptions
            { 
                Expires = tokenResult.Value.RefreshTokenExpiryTime,
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            });

        return new AccessTokenDto(tokenResult.Value.Token);
    }
}