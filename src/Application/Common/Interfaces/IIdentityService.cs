using CheckflixApp.Domain.Common.Primitives.Result;
using Microsoft.AspNetCore.Identity;

namespace CheckflixApp.Application.Common.Interfaces;
public interface IIdentityService
{
    Task<string> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, IdentityUser user)> CreateUserAsync(string email, string userName, string? password);

    Task<Result> DeleteUserAsync(string userId);

    Task<bool> IsEmailUniqueAsync(string email);

    Task<bool> IsUserNameUniqueAsync(string email);

    Task<IdentityUser?> GetIdentityUserByEmailAsync(string email);
}
