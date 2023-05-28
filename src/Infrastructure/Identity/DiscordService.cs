using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;
using AspNet.Security.OAuth.Discord;
using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Domain.Common.Primitives;
using CheckflixApp.Domain.Common.Primitives.Result;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Infrastructure.Identity;
public class DiscordService : IDiscordService
{
    private readonly HttpClient _httpClient;
    private readonly IStringLocalizer<DiscordService> _localizer;

    public DiscordService(HttpClient httpClient, IStringLocalizer<DiscordService> localizer)
    {
        _httpClient = httpClient;
        _localizer = localizer;
    }

    public async Task<Result<ProviderTokenDto>> GetTokenFromDiscordAsync(string authorizationCode)
    {
        var parameters = new Dictionary<string, string>
        {
            { "client_id", "1112364224820297800" },
            { "client_secret", "jTF4NSq8RNcGhGPNwZK12N9h5Iuc0hC-" },
            { "grant_type", "authorization_code" },
            { "code", authorizationCode },
            { "redirect_uri", "https://localhost:5001/api/Tokens/discord-callback" },
            { "scope", "identify email" }
        };

        var response = await _httpClient.PostAsync(DiscordAuthenticationDefaults.TokenEndpoint, new FormUrlEncodedContent(parameters));
        var content = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonSerializer.Deserialize<DiscordTokenResponse>(content);

        if (tokenResponse == null || !response.IsSuccessStatusCode)
        {
            return Error.Validation(_localizer["Failed to obtain discord access token"]);
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
