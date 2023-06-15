using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Application.Identity.Tokens.Queries.GetToken;
using CheckflixApp.Domain.Common.Primitives;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.Infrastructure.Auth;
using CheckflixApp.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CheckflixApp.Infrastructure.Identity;
public class TokenService : ITokenService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IStringLocalizer<TokenService> _localizer;
    private readonly JwtSettings _jwtSettings;
    private readonly SecuritySettings _securitySettings;

    public TokenService(
        UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> jwtSettings,
        IStringLocalizer<TokenService> localizer,
        IOptions<SecuritySettings> securitySettings)
    {
        _userManager = userManager;
        _localizer = localizer;
        _jwtSettings = jwtSettings.Value;
        _securitySettings = securitySettings.Value;
    }

    public async Task<Result<TokenDto>> GetTokenAsync(GetTokenQuery query, string ipAddress, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(query.Email.Trim().Normalize());

        if (user is null)
        {
            return Error.NotFound(description: _localizer["auth.failed"]);
        }

        if (!user.IsActive)
        {
            return Error.Validation(description: _localizer["identity.usernotactive"]);
        }

        if (_securitySettings.RequireConfirmedAccount && !user.EmailConfirmed)
        {
            return Error.Unauthorized(description: _localizer["identity.emailnotconfirmed"]);
        }

        if (!await _userManager.CheckPasswordAsync(user, query.Password))
        {
            return Error.Unauthorized(description: _localizer["identity.invalidcredentials"]);
        }

        return await GenerateTokensAndUpdateUser(user, ipAddress);
    }

    public async Task<Result<TokenDto>> GetTokenByEmailAsync(string email, string ipAddress, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return Error.NotFound(description: _localizer["auth.failed"]);
        }

        return await GenerateTokensAndUpdateUser(user, ipAddress);
    }

    public async Task<Result<TokenDto>> GetRefreshTokenAsync(string accessToken, string refreshToken, string ipAddress, CancellationToken cancellationToken)
    {
        var userPrincipal = GetPrincipalFromExpiredToken(accessToken);
        if (userPrincipal.IsFailure)
        {
            return userPrincipal.Errors;
        }

        string? userEmail = userPrincipal.Value.GetEmail();
        if (userEmail is null)
        {
            return Error.NotFound(description: _localizer["auth.usernotfound"]);
        }

        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user is null)
        {
            return Error.Unauthorized(description: _localizer["auth.failed"]);
        }

        if (user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return Error.Unauthorized(description: _localizer["identity.invalidrefreshtoken"]);
        }

        return await GenerateTokensAndUpdateUser(user, ipAddress);
    }

    private async Task<TokenDto> GenerateTokensAndUpdateUser(ApplicationUser user, string ipAddress)
    {
        string token = GenerateJwt(user, ipAddress);

        user.RefreshToken = GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays);

        await _userManager.UpdateAsync(user);

        return new TokenDto(token, user.RefreshToken, user.RefreshTokenExpiryTime);
    }

    private string GenerateJwt(ApplicationUser user, string ipAddress) =>
        GenerateEncryptedToken(GetSigningCredentials(), GetClaims(user, ipAddress));

    private IEnumerable<Claim> GetClaims(ApplicationUser user, string ipAddress) =>
        new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email ?? string.Empty),
            new(ClaimTypes.Name, user.UserName ?? string.Empty),
            new(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty),
            //new("Avatar", user.Avatar ?? string.Empty),
            new("ipAddress", ipAddress ?? string.Empty),
        };

    private string GenerateRefreshToken()
    {
        byte[] randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
    {
        var token = new JwtSecurityToken(
           claims: claims,
           expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
           signingCredentials: signingCredentials,
           issuer: _jwtSettings.Issuer,
           audience: _jwtSettings.Audience);
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    private Result<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
    {
        if (string.IsNullOrEmpty(_jwtSettings.Secret))
        {
            return Error.Failure(description: _localizer["No Key defined in JwtSettings config."]);
        }

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            ValidateIssuer = false,
            ValidateAudience = false,
            RoleClaimType = ClaimTypes.Role,
            ClockSkew = TimeSpan.Zero,
            ValidateLifetime = false
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            return Error.Unauthorized(description: _localizer["identity.invalidtoken"]);
        }

        return principal;
    }

    private SigningCredentials GetSigningCredentials()
    {
        if (string.IsNullOrEmpty(_jwtSettings.Secret))
        {
            throw new InvalidOperationException("No Key defined in JwtSettings config.");
        }

        byte[] secret = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
        return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
    }
}