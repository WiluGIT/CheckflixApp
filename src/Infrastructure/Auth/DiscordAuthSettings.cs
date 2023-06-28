namespace CheckflixApp.Infrastructure.Auth;
public class DiscordAuthSettings
{
    public string ClientId { get; init; } = string.Empty;
    public string ClientSecret { get; init; } = string.Empty;
    public string RedirectUri { get; init; } = string.Empty;
    public string Scope { get; init; } = string.Empty;
}
