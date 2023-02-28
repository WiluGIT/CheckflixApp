using AutoMapper;
using CheckflixApp.Application.Identity.Common;
using Microsoft.AspNetCore.Identity;

namespace CheckflixApp.Infrastructure.Identity;
public class ApplicationUser : IdentityUser
{
    public bool IsActive { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public string? ImageUrl { get; set; }
    public string? ObjectId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserDetailsDto, ApplicationUser>()
            .ReverseMap();
    }
}
