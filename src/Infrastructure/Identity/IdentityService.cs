using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Domain.Common.Primitives.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CheckflixApp.Infrastructure.Identity;
public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    
    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
    }

    public async Task<string> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<(Result Result, string UserId)> CreateUserAsync(string email, string userName, string? password)
    {
        var user = ApplicationUser.Create(
            username: userName,
            email: email,
            isActive: true,
            emailConfirmed: true);

        var result = await CreateUserWithRole(user, password, SystemRoles.Basic);

        return (result.ToApplicationResult(), user.Id);
    }
    
    private async Task<IdentityResult> CreateUserWithRole(ApplicationUser user, string? password, string role)
    {
        IdentityResult? result = password is null ? await _userManager.CreateAsync(user) : await _userManager.CreateAsync(user, password);
        await _userManager.AddToRoleAsync(user, role);

        return result;
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null ? await DeleteUserAsync(user) : Result.From();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    public async Task<bool> IsEmailUniqueAsync(string email) => 
        await _userManager.FindByEmailAsync(email) == null;

    public async Task<bool> IsUserNameUniqueAsync(string userName) =>
        await _userManager.FindByNameAsync(userName) == null;
}
