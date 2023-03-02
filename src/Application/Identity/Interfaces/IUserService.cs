﻿using CheckflixApp.Application.Common.Models;
using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Users.Commands.ChangePassword;
using CheckflixApp.Application.Identity.Users.Commands.CreateUser;
using CheckflixApp.Application.Identity.Users.Commands.ForgotPassword;
using CheckflixApp.Application.Identity.Users.Commands.ResetPassword;
using CheckflixApp.Application.Identity.Users.Commands.ToggleUserStatus;
using CheckflixApp.Application.Identity.Users.Commands.UpdateUser;

namespace CheckflixApp.Application.Identity.Interfaces;
public interface IUserService
{
    Task<PaginatedList<UserDetailsDto>> SearchAsync(UserListFilter filter, CancellationToken cancellationToken);

    Task<bool> ExistsWithNameAsync(string name);
    Task<bool> ExistsWithEmailAsync(string email, string? exceptId = null);
    Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber, string? exceptId = null);

    Task<List<UserDetailsDto>> GetListAsync(CancellationToken cancellationToken);

    Task<int> GetCountAsync(CancellationToken cancellationToken);

    Task<UserDetailsDto> GetAsync(string userId, CancellationToken cancellationToken);

    Task<List<UserRoleDto>> GetRolesAsync(string userId, CancellationToken cancellationToken);
    
    Task<string> AssignRolesAsync(string userId, UserRolesRequest request, CancellationToken cancellationToken);

    //Task<List<string>> GetPermissionsAsync(string userId, CancellationToken cancellationToken);
    //Task<bool> HasPermissionAsync(string userId, string permission, CancellationToken cancellationToken = default);
    //Task InvalidatePermissionCacheAsync(string userId, CancellationToken cancellationToken);

    Task ToggleUserStatusAsync(ToggleUserStatusCommand command, CancellationToken cancellationToken);

    //Task<string> GetOrCreateFromPrincipalAsync(ClaimsPrincipal principal);
    Task<string> CreateAsync(CreateUserCommand command, string origin);
    Task UpdateAsync(UpdateUserCommand command, string userId);

    Task<string> ConfirmEmailAsync(string userId, string code, string tenant, CancellationToken cancellationToken);
    Task<string> ConfirmPhoneNumberAsync(string userId, string code);

    Task<string> ForgotPasswordAsync(ForgotPasswordCommand command, string origin);
    Task<string> ResetPasswordAsync(ResetPasswordCommand command);
    Task ChangePasswordAsync(ChangePasswordCommand command, string userId);
}