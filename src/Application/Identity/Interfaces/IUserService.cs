using System.Security.Claims;
using CheckflixApp.Application.Common.Models;
using CheckflixApp.Application.Followings.Common;
using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Personal.Commands.ChangePassword;
using CheckflixApp.Application.Identity.Personal.Commands.UpdateUser;
using CheckflixApp.Application.Identity.Users.Commands.CreateUser;
using CheckflixApp.Application.Identity.Users.Commands.ForgotPassword;
using CheckflixApp.Application.Identity.Users.Commands.ResetPassword;
using CheckflixApp.Application.Identity.Users.Commands.ToggleUserStatus;
using CheckflixApp.Domain.Common.Primitives.Result;
using Microsoft.AspNetCore.Identity;

namespace CheckflixApp.Application.Identity.Interfaces;
public interface IUserService
{
    Task<PaginatedList<UserDetailsDto>> SearchAsync(UserListFilter filter, CancellationToken cancellationToken);
    Task<bool> ExistsWithNameAsync(string name);
    Task<bool> ExistsWithEmailAsync(string email, string? exceptId = null);
    Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber, string? exceptId = null);
    Task<List<UserDetailsDto>> GetListAsync(CancellationToken cancellationToken);
    Task<int> GetCountAsync(CancellationToken cancellationToken);
    Task<UserFollowingsCountDto?> GetFollowingCountAsync(string userId, CancellationToken cancellationToken);
    Task<UserDetailsDto?> GetAsync(string userId, CancellationToken cancellationToken);
    Task<Result<List<UserRoleDto>>> GetRolesAsync(string userId, CancellationToken cancellationToken);    
    Task<Result<string>> AssignRolesAsync(string userId, UserRolesRequest request, CancellationToken cancellationToken);
    Task<Result> ToggleUserStatusAsync(ToggleUserStatusCommand command, CancellationToken cancellationToken);
    Task<Result<string>> GetOrCreateFromPrincipalAsync(ClaimsPrincipal principal);
    Task<Result<string>> CreateAsync(CreateUserCommand command, string origin);
    Task<Result<string>> UpdateAsync(UpdateUserCommand command, string userId);
    Task<Result<string>> ConfirmEmailAsync(string userId, string code, CancellationToken cancellationToken);
    Task<Result<string>> ConfirmPhoneNumberAsync(string userId, string code);
    Task<Result<string>> ForgotPasswordAsync(ForgotPasswordCommand command, string origin);
    Task<Result<string>> ResetPasswordAsync(ResetPasswordCommand command);
    Task<Result<string>> ChangePasswordAsync(ChangePasswordCommand command, string userId);
    Task<IdentityUser?> GetUserByEmailAsync(string email);
}
