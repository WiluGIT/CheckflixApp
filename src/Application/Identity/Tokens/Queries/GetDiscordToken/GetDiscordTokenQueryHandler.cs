using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Domain.Common.Primitives;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.Identity.Tokens.Queries.GetDiscordToken;

public class GetDiscordTokenQueryHandler : IRequestHandler<GetDiscordTokenQuery, Result<TokenDto>>
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

    public async Task<Result<TokenDto>> Handle(GetDiscordTokenQuery query, CancellationToken cancellationToken)
    {
        var ipAddress = _httpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For")
            ? _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"].ToString()
            : _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "N/A";

        var providerToken = await _discordService.GetTokenFromDiscordAsync(query.Code);

        if (providerToken.IsFailure)
        {
            return providerToken.Errors;
        }

        if (await _identityService.IsEmailUniqueAsync(providerToken.Value.Email))
        {
            var (result, id) = await _identityService.CreateUserAsync(providerToken.Value.Email, null);
            if (result.IsFailure)
            {
                return Error.Failure(_localizer["Failed to create user"]);
            }
        }

        return await _tokenService.GetTokenByEmailAsync(providerToken.Value.Email, ipAddress, cancellationToken);
    }
}