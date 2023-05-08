using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CheckflixApp.Application.Identity.Tokens.Queries.GetRefreshToken;

public class GetRefreshTokenQueryHandler : IRequestHandler<GetRefreshTokenQuery, Result<TokenDto>>
{
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public GetRefreshTokenQueryHandler(ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
    {
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<TokenDto>> Handle(GetRefreshTokenQuery query, CancellationToken cancellationToken)
    {
        var ipAddress = _httpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For")
            ? _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"].ToString()
            : _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "N/A";

        var tokenResult = await _tokenService.GetRefreshTokenAsync(query, ipAddress, cancellationToken);

        if (tokenResult.IsFailure)
        {
            return tokenResult.Errors;
        }

        return tokenResult;
    }
}