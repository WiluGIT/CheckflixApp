using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Tokens.Queries.GetRefreshToken;
using CheckflixApp.Application.Identity.Tokens.Queries.GetToken;
using CheckflixApp.Domain.Common.Primitives.Result;

namespace CheckflixApp.Application.Identity.Interfaces;

public interface IDiscordService
{
    Task<Result<ProviderTokenDto>> GetTokenFromDiscordAsync(string authorizationCode, string baseUrl);
}