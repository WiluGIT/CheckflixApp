using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Roles.Commands.CreateOrUpdateRole;

namespace CheckflixApp.Application.Identity.Interfaces;
public interface IRoleService
{
    Task<List<RoleDto>> GetListAsync(CancellationToken cancellationToken);

    Task<int> GetCountAsync(CancellationToken cancellationToken);

    Task<bool> ExistsAsync(string roleName, string? excludeId);

    Task<RoleDto> GetByIdAsync(string id);

    Task<string> CreateOrUpdateAsync(CreateOrUpdateRoleCommand command);

    Task<string> DeleteAsync(string id);
}