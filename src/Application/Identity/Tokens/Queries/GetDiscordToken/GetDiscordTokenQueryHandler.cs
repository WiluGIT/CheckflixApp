using System.Runtime.CompilerServices;
using AutoMapper;
using CheckflixApp.Application.Common.Extensions;
using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Domain.Common.Consts;
using CheckflixApp.Domain.Common.Primitives;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.Identity.Tokens.Queries.GetDiscordToken;

public class GetDiscordTokenQueryHandler : IRequestHandler<GetDiscordTokenQuery, Result<string>>
{
    private readonly ITokenService _tokenService;
    private readonly IDiscordService _discordService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IIdentityService _identityService;
    private readonly IStringLocalizer<GetDiscordTokenQueryHandler> _localizer;

    public GetDiscordTokenQueryHandler(
        ITokenService tokenService,
        IHttpContextAccessor httpContextAccessor,
        IDiscordService discordService,
        IIdentityService identityService,
        IStringLocalizer<GetDiscordTokenQueryHandler> localizer)
    {
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
        _discordService = discordService;
        _identityService = identityService;
        _localizer = localizer;
    }

    public async Task<Result<string>> Handle(GetDiscordTokenQuery query, CancellationToken cancellationToken)
    {
        var ipAddress = _httpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For")
            ? _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"].ToString()
            : _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "N/A";

        var baseUrl = _httpContextAccessor.HttpContext.GetBaseUrl();
        var providerToken = await _discordService.GetTokenFromDiscordAsync(query.Code, baseUrl);
        if (providerToken.IsFailure)
        {
            return providerToken.Errors;
        }

        var user = await _identityService.GetIdentityUserByEmailAsync(providerToken.Value.Email);
        if (user is null)
        {
            var (result, createdUser) = await _identityService.CreateUserAsync(providerToken.Value.Email, providerToken.Value.Email, null);
            user = createdUser;

            if (result.IsFailure)
            {
                return Error.Failure(_localizer["Failed to create user"]);
            }
        }

        var tokenResult = await _tokenService.GetTokenByEmailAsync(providerToken.Value.Email, ipAddress, cancellationToken);
        if (tokenResult.IsFailure)
        {
            return tokenResult.Errors;
        }

        _tokenService.SetRefreshTokenHttpOnlyCookie(tokenResult.Value.RefreshToken, tokenResult.Value.RefreshTokenExpiryTime);

        var queryParams = new Dictionary<string, string?>()
        {
            { AuthKeys.AccessToken, tokenResult.Value.Token },
        };

        var redirectUrl = _tokenService.GetAuthRedirectUrl(queryParams);

        return redirectUrl;
    }
}