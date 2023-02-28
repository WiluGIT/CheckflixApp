using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckflixApp.Infrastructure.Auth;
public class JwtSettings
{
    public string Secret { get; init; } = null!;
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public int ExpiryMinutes { get; init; }
    public int RefreshTokenExpirationInDays { get; set; }
}
