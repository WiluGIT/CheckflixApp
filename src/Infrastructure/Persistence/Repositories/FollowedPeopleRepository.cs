using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheckflixApp.Infrastructure.Persistence.Repositories;

/// <summary>
/// Represents the FollowedPeople repository.
/// </summary>
internal sealed class FollowedPeopleRepository : GenericRepository<FollowedPeople>, IFollowedPeopleRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FollowingRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public FollowedPeopleRepository(IApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<FollowedPeople>> GetUserFollowingsByIdAsync(string userId) =>
        await DbContext.Set<FollowedPeople>()
            .AsNoTracking()
            .Where(x => x.ObserverId.Equals(userId))
            .ToListAsync();
        
    public async Task<FollowedPeople?> GetFollowing(string observerId, string targetId) =>
        await DbContext.Set<FollowedPeople>()
            .Where(x => x.ObserverId.Equals(observerId) && x.TargetId.Equals(targetId))
            .FirstOrDefaultAsync();

    public async Task<bool> ValidateFollowing(string observerId, string targetId) =>
        await DbContext.Set<FollowedPeople>()
            .AnyAsync(x => x.ObserverId.Equals(observerId) && x.TargetId.Equals(targetId));
}