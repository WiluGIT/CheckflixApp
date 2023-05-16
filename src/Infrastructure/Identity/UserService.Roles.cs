using CheckflixApp.Application.Common.Exceptions;
using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Domain.Common.Primitives;
using CheckflixApp.Domain.Common.Primitives.Result;
using Microsoft.EntityFrameworkCore;

namespace CheckflixApp.Infrastructure.Identity;
internal partial class UserService
{
    public async Task<Result<List<UserRoleDto>>> GetRolesAsync(string userId, CancellationToken cancellationToken)
    {
        var userRoles = new List<UserRoleDto>();

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return Error.NotFound(description: _localizer["User Not Found."]);
        }

        var roles = await _roleManager.Roles.AsNoTracking().ToListAsync(cancellationToken);
        foreach (var role in roles)
        {
            userRoles.Add(new UserRoleDto
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Enabled = await _userManager.IsInRoleAsync(user, role.Name)
            });
        }

        return userRoles;
    }

    public async Task<Result<string>> AssignRolesAsync(string userId, UserRolesRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            return Error.NotFound(description: _localizer["User Not Found."]);
        }

        if (await _userManager.IsInRoleAsync(user, SystemRoles.Administrator)
            && request.UserRoles.Any(a => !a.Enabled && a.RoleName == SystemRoles.Administrator))
        {
            int adminCount = (await _userManager.GetUsersInRoleAsync(SystemRoles.Administrator)).Count;

            if (adminCount <= 1)
            {
                return Error.Validation(description: _localizer["Application should have at least 1 Admin."]);
            }
        }

        foreach (var userRole in request.UserRoles)
        {
            if (await _roleManager.FindByNameAsync(userRole.RoleName) is null)
            {
                return Error.NotFound(description: _localizer["Role not found."]);
            }

            await UpdateRoleAssigment(user, userRole.RoleName, userRole.Enabled);
        }

        //await _events.PublishAsync(new ApplicationUserUpdatedEvent(user.Id, true));

        return _localizer["User Roles Updated Successfully."].Value;
    }

    private async Task UpdateRoleAssigment(ApplicationUser user, string roleName, bool isEnabled)
    {
        if (!isEnabled)
        {
            await _userManager.RemoveFromRoleAsync(user, roleName);
            return;
        }

        if (!await _userManager.IsInRoleAsync(user, roleName))
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }
    }
}