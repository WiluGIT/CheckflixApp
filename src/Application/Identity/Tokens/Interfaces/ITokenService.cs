using CheckflixApp.Application.Identity.Dtos;
using CheckflixApp.Application.Identity.Tokens.Queries.GetRefreshToken;
using CheckflixApp.Application.Identity.Tokens.Queries.GetToken;

namespace CheckflixApp.Application.Identity.Tokens.Interfaces;
public interface ITokenService
{
    Task<TokenDto> GetTokenAsync(GetTokenQuery query, string ipAddress, CancellationToken cancellationToken);

    Task<TokenDto> GetRefreshTokenAsync(GetRefreshTokenQuery query, string ipAddress);
}