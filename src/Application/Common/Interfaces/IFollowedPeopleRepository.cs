using CheckflixApp.Application.Followings.Common;
using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Domain.Entities;

namespace CheckflixApp.Application.Common.Interfaces;

/// <summary>
/// Represents the invitation repository interface.
/// </summary>
public interface IFollowedPeopleRepository
{
    /// <summary>
    /// Gets the user followings with the specified identifier.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns>Collection of followings for specified users.</returns>
    Task<List<UserWithFollowingDto>> GetUserFollowingsByIdAsync(string userId);

    /// <summary>
    /// Gets the user followers with the specified identifier.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns>Collection of followers for specified users.</returns>
    Task<List<UserWithFollowingDto>> GetUserFollowersByIdAsync(string userId);

    /// <summary>
    /// Gets the user following with the specified observer and target identifier.
    /// </summary>
    /// <param name="observerId">The observer identifier.</param>
    /// <param name="targetId">The target identifier.</param>
    /// <returns></returns>
    Task<FollowedPeople?> GetFollowing(string observerId, string targetId);

    /// <summary>
    /// Validates if following for specified identifiers exists.
    /// </summary>
    /// <param name="observerId">The observer identifier.</param>
    /// <param name="targetId">The target identifier.</param>
    /// <returns>The validation result.</returns>
    Task<bool> ValidateFollowing(string observerId, string targetId);

    /// <summary>
    /// Inserts the specified FollowedPeople to the database.
    /// </summary>
    /// <param name="production">The FollowedPeople to be inserted into the database.</param>
    void Insert(FollowedPeople followedPeople);


    /// <summary>
    /// Removes the specified FollowedPeople from the database.
    /// </summary>
    /// <param name="friendship">The FollowedPeople to be removed from the database.</param>
    void Remove(FollowedPeople followedPeople);

    /// <summary>
    /// Searches for users with following information.
    /// </summary>
    /// <param name="friendship">The UserDto with following information.</param>
    Task<List<UserWithFollowingDto>> SearchUsersWithFollowingAsync(string searchQuery, string userId, int count);
}