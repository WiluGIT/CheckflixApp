using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;
using AspNet.Security.OAuth.Discord;
using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Domain.Common.Consts;
using CheckflixApp.Domain.Common.Primitives;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.Infrastructure.Auth;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace CheckflixApp.Infrastructure.Identity;
public class DiscordService : IDiscordService
{
    private readonly HttpClient _httpClient;
    private readonly IStringLocalizer<DiscordService> _localizer;
    private readonly DiscordAuthSettings _discordAuthSettings;

    public DiscordService(HttpClient httpClient, IStringLocalizer<DiscordService> localizer, IOptions<DiscordAuthSettings> discordAuthSettings)
    {
        _httpClient = httpClient;
        _localizer = localizer;
        _discordAuthSettings = discordAuthSettings.Value;
    }

    public async Task<Result<ProviderTokenDto>> GetTokenFromDiscordAsync(string authorizationCode, string baseUrl)
    {
        var parameters = new Dictionary<string, string>
        {
            { AuthKeys.ClientId, _discordAuthSettings.ClientId },
            { AuthKeys.ClientSecret, _discordAuthSettings.ClientSecret },
            { AuthKeys.GrantType, AuthKeys.AuthorizationCode },
            { AuthKeys.Code, authorizationCode },
            // we need our base app path to concat with token discord callback endpoint
            { AuthKeys.RedirectUri, $"{baseUrl}{_discordAuthSettings.RedirectUri}"},
            { AuthKeys.Scope, _discordAuthSettings.Scope }
            //"https://localhost:5001/api/Tokens/discord-callback"
        };

        var response = await _httpClient.PostAsync(DiscordAuthenticationDefaults.TokenEndpoint, new FormUrlEncodedContent(parameters));
        var content = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonSerializer.Deserialize<DiscordTokenResponse>(content);

        if (tokenResponse == null || !response.IsSuccessStatusCode)
        {
            return Error.Validation(description: $"{_localizer["Failed to obtain discord access token"]} details: {content}");
        }

        using var requestMessage = new HttpRequestMessage(HttpMethod.Get, DiscordAuthenticationDefaults.UserInformationEndpoint);
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
        var userResponse = await _httpClient.SendAsync(requestMessage);
        var userContent = await userResponse.Content.ReadAsStringAsync();
        var discordUser = JsonSerializer.Deserialize<DiscordUser>(userContent);

        if (discordUser == null || !userResponse.IsSuccessStatusCode)
        {
            return Error.Validation(_localizer["Failed to obtain user information"]);
        }
        discordUser.Avatar = GetAvatarUrl(discordUser);

        return new ProviderTokenDto(tokenResponse.AccessToken, discordUser.Username, discordUser.Email, discordUser.Avatar);
    }

    private string GetAvatarUrl(DiscordUser discordUser) =>
        string.Format(
            CultureInfo.InvariantCulture,
            "https://cdn.discordapp.com/avatars/{0}/{1}.{2}",
            discordUser.Id,
            discordUser.Avatar,
            discordUser.Avatar.StartsWith("a_") ? "gif" : "png"
        );
}
