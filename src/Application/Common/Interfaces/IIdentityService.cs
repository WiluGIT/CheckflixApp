using CheckflixApp.Domain.Common.Primitives.Result;

namespace CheckflixApp.Application.Common.Interfaces;
public interface IIdentityService
{
    Task<string> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result<Success> Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<Result<Success>> DeleteUserAsync(string userId);

    Task<bool> IsEmailUniqueAsync(string email);
}
