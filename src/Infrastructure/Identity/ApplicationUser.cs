using AutoMapper;
using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CheckflixApp.Infrastructure.Identity;
public class ApplicationUser : IdentityUser
{
    public bool IsActive { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public string? ImageUrl { get; set; }
    public string? ObjectId { get; set; }

    public List<FollowedPeople> Following { get; set; } = new();
    public List<FollowedPeople> Followers { get; set; } = new();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserDetailsDto, ApplicationUser>()
            .ReverseMap();
    }
}
