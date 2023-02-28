namespace CheckflixApp.Application.Identity.Common;
public record TokenDto(string Token, string RefreshToken, DateTime RefreshTokenExpiryTime);
