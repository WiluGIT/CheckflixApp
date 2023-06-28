using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Tokens.Queries.GetToken;
using CheckflixApp.Domain.Common.Primitives.Result;

namespace CheckflixApp.Application.Identity.Interfaces;
public interface ITokenService
{
    Task<Result<TokenDto>> GetTokenAsync(GetTokenQuery query, string ipAddress, CancellationToken cancellationToken);

    Task<Result<TokenDto>> GetRefreshTokenAsync(string accessToken, string refreshToken, string ipAddress, CancellationToken cancellationToken);

    Task<Result<TokenDto>> GetTokenByEmailAsync(string email, string ipAddress, CancellationToken cancellationToken);

    void SetRefreshTokenHttpOnlyCookie(string refreshToken, DateTime refreshTokenExpiryTime);

    string GetAuthRedirectUrl(Dictionary<string, string?> queryParams);
}