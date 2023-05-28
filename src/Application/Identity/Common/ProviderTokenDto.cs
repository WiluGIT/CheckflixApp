namespace CheckflixApp.Application.Identity.Common;

public record ProviderTokenDto(string AccessToken, string UserName, string Email, string Avatar);