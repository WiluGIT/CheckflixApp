using AutoMapper;
using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Domain.Common.Consts;
using CheckflixApp.Domain.Common.Primitives;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.Identity.Tokens.Queries.GetToken;
public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, Result<AccessTokenDto>>
{
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetTokenQueryHandler> _localizer;

    public GetTokenQueryHandler(
        ITokenService tokenService,
        IHttpContextAccessor httpContextAccessor,
        IUserService userService,
        IMapper mapper,
        IStringLocalizer<GetTokenQueryHandler> localizer)
    {
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
        _userService = userService;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<Result<AccessTokenDto>> Handle(GetTokenQuery query, CancellationToken cancellationToken)
    {
        var ipAddress = _httpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For")
            ? _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"].ToString()
            : _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "N/A";

        var user = await _userService.GetUserByEmailAsync(query.Email);

        if (user is null)
        {
            return Error.NotFound(description: _localizer["auth.failed"]);
        }

        var tokenResult = await _tokenService.GetTokenAsync(query, ipAddress, cancellationToken);

        if (tokenResult.IsFailure)
        {
            return tokenResult.Errors;
        }

        _tokenService.SetRefreshTokenHttpOnlyCookie(tokenResult.Value.RefreshToken, tokenResult.Value.RefreshTokenExpiryTime);

        return new AccessTokenDto(tokenResult.Value.Token);
    }
}