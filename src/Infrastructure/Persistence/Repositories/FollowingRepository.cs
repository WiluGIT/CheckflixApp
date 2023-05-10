﻿using Microsoft.EntityFrameworkCore;
using static CheckflixApp.Domain.Common.Errors.DomainErrors;

namespace CheckflixApp.Infrastructure.Persistence.Repositories;

/// <summary>
/// Represents the invitation repository.
/// </summary>
internal sealed class FollowingRepository : GenericRepository<Invitation>, IInvitationRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvitationRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public FollowingRepository(IDbContext dbContext)
        : base(dbContext)
    {
    }

    /// <inheritdoc />
    public async Task<bool> CheckIfInvitationAlreadySentAsync(GroupEvent groupEvent, User user) =>
        await AnyAsync(new PendingInvitationSpecification(groupEvent, user));

    /// <inheritdoc />
    public async Task RemovePendingInvitationsForFriendshipAsync(Friendship friendship, DateTime utcNow)
    {
        const string sql = @"
                UPDATE Invitation
                SET DeletedOnUtc = @DeletedOn, Deleted = @Deleted
                WHERE (UserId = @UserId AND FriendId = @FriendId) ||
                      (UserId = @FriendId AND FriendId = @UserId)
                      CompletedOnUtc IS NULL AND Deleted = 0";

        SqlParameter[] parameters =
        {
                new SqlParameter("@DeletedOn", utcNow),
                new SqlParameter("@Deleted", true),
                new SqlParameter("@UserId", friendship.UserId),
                new SqlParameter("@FriendId", friendship.FriendId)
            };

        await DbContext.ExecuteSqlAsync(sql, parameters);
    }

    /// <inheritdoc />
    public async Task RemoveInvitationsForGroupEventAsync(GroupEvent groupEvent, DateTime utcNow)
    {
        const string sql = @"
                UPDATE Invitation
                SET DeletedOnUtc = @DeletedOn, Deleted = @Deleted
                WHERE EventId = @EventId AND Deleted = 0";

        SqlParameter[] parameters =
        {
                new SqlParameter("@DeletedOn", utcNow),
                new SqlParameter("@Deleted", true),
                new SqlParameter("@EventId", groupEvent.Id)
            };

        await DbContext.ExecuteSqlAsync(sql, parameters);
    }
}