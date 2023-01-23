using Microsoft.AspNetCore.Identity;

namespace CheckflixApp.Infrastructure.Identity;
public class ApplicationUser : IdentityUser
{
    public bool IsActive { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}
