using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Followings.Common;
using CheckflixApp.Domain.Entities;
using CheckflixApp.Infrastructure.Identity;
using MailKit.Search;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CheckflixApp.Infrastructure.Persistence.Repositories;

/// <summary>
/// Represents the FollowedPeople repository.
/// </summary>
internal sealed class FollowedPeopleRepository : GenericRepository<FollowedPeople>, IFollowedPeopleRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    /// <summary>
    /// Initializes a new instance of the <see cref="FollowingRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public FollowedPeopleRepository(IApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        : base(dbContext)
    {
        _userManager = userManager;
    }

    public async Task<List<UserWithFollowingDto>> GetUserFollowingsByIdAsync(string userId)
        => await (
                from au in _userManager.Users
                where au.Followers.Any(x => x.ObserverId == userId)
                select au)
                .Select(au => new UserWithFollowingDto
                {
                    Id = au.Id,
                    UserName = au.UserName ?? string.Empty,
                    IsFollowing = true
                }).ToListAsync();

    public async Task<List<UserWithFollowingDto>> GetUserFollowersByIdAsync(string userId)
    => await (
            from au in _userManager.Users
            where au.Following.Any(x => x.TargetId == userId)
            select au)
            .Select(au => new UserWithFollowingDto
            {
                Id = au.Id,
                UserName = au.UserName ?? string.Empty,
                IsFollowing = false
            }).ToListAsync();

    public async Task<FollowedPeople?> GetFollowing(string observerId, string targetId) =>
        await DbContext.Set<FollowedPeople>()
            .Where(x => x.ObserverId.Equals(observerId) && x.TargetId.Equals(targetId))
            .FirstOrDefaultAsync();

    public async Task<bool> ValidateFollowing(string observerId, string targetId) =>
        await DbContext.Set<FollowedPeople>()
            .AnyAsync(x => x.ObserverId.Equals(observerId) && x.TargetId.Equals(targetId));

    public async Task<List<UserWithFollowingDto>> SearchUsersWithFollowingAsync(string searchQuery, string userId, int count)
    {
        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            return await (
                from au in _userManager.Users
                where au.UserName != null && au.UserName.ToLower().Contains(searchQuery.ToLower())
                select au)
                .Take(count)
                .Select(au => new UserWithFollowingDto
                {
                    Id = au.Id,
                    UserName = au.UserName ?? string.Empty,
                    IsFollowing = au.Followers.Any(x => x.ObserverId == userId)
                }).ToListAsync();
        }

        return await (from au in _userManager.Users select au)
            .Take(count)
            .Select(au => new UserWithFollowingDto
            {
                Id = au.Id,
                UserName = au.UserName ?? string.Empty,
                IsFollowing = au.Followers.Any(x => x.ObserverId == userId)
            }).ToListAsync();
    }
}