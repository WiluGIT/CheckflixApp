using CheckflixApp.Application.Common.Exceptions;
using CheckflixApp.Application.Identity.Common;
using Microsoft.EntityFrameworkCore;

namespace CheckflixApp.Infrastructure.Identity;
internal partial class UserService
{
    public async Task<List<UserRoleDto>> GetRolesAsync(string userId, CancellationToken cancellationToken)
    {
        var userRoles = new List<UserRoleDto>();

        var user = await _userManager.FindByIdAsync(userId);
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

    public async Task<string> AssignRolesAsync(string userId, UserRolesRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var user = await _userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException(_localizer["User Not Found."]);

        if (await _userManager.IsInRoleAsync(user, SystemRoles.Administrator)
            && request.UserRoles.Any(a => !a.Enabled && a.RoleName == SystemRoles.Administrator))
        {
            int adminCount = (await _userManager.GetUsersInRoleAsync(SystemRoles.Administrator)).Count;

            if (adminCount <= 1)
            {
                throw new InternalServerException(_localizer["Application should have at least 1 Admin."]);
            }
        }

        foreach (var userRole in request.UserRoles)
        {
            if (await _roleManager.FindByNameAsync(userRole.RoleName) is null)
            {
                return _localizer["Role not found."];
            }

            await UpdateRoleAssigment(user, userRole.RoleName, userRole.Enabled);
        }

        //await _events.PublishAsync(new ApplicationUserUpdatedEvent(user.Id, true));

        return _localizer["User Roles Updated Successfully."];
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