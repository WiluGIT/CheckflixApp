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

    private readonly List<FollowedPeople> _following = new();
    private readonly List<FollowedPeople> _followers = new();
    private readonly List<ApplicationUserGenre> _applicationUserGenres = new();
    private readonly List<ApplicationUserNotification> _applicationUserNotifications = new();
    private readonly List<ApplicationUserProduction> _applicationUserProductions = new();
    public IReadOnlyList<FollowedPeople> Following => _following.AsReadOnly();
    public IReadOnlyList<FollowedPeople> Followers => _followers.AsReadOnly();
    public IReadOnlyList<ApplicationUserGenre> ApplicationUserGenres => _applicationUserGenres.AsReadOnly();
    public IReadOnlyList<ApplicationUserNotification> ApplicationUserNotifications => _applicationUserNotifications.AsReadOnly();
    public IReadOnlyList<ApplicationUserProduction> ApplicationUserProductions => _applicationUserProductions.AsReadOnly();


    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserDetailsDto, ApplicationUser>()
        .ReverseMap();
    }

    private ApplicationUser(
        string email,
        string username,
        bool isActive,
        bool emailConfirmed,
        string? objectId)
    {
        ObjectId = objectId;
        Email = email;
        NormalizedEmail = email.ToUpperInvariant();
        UserName = username;
        NormalizedUserName = username.ToUpperInvariant();
        IsActive = isActive;
        EmailConfirmed = emailConfirmed;
    }

    public static ApplicationUser Create(
        string email,
        string username,
        bool isActive,
        bool emailConfirmed,
        string? objectId = null)
    {
        return new(
            email: email, 
            username: username, 
            isActive: isActive, 
            emailConfirmed: emailConfirmed, 
            objectId: objectId);
    }

    #pragma warning disable CS8618 // Required by Entity Framework
    private ApplicationUser() { }
}
