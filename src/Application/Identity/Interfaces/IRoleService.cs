using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Roles.Commands.CreateOrUpdateRole;
using CheckflixApp.Domain.Common.Primitives.Result;

namespace CheckflixApp.Application.Identity.Interfaces;
public interface IRoleService
{
    Task<List<RoleDto>> GetListAsync(CancellationToken cancellationToken);

    Task<int> GetCountAsync(CancellationToken cancellationToken);

    Task<bool> ExistsAsync(string roleName, string? excludeId);

    Task<Result<RoleDto>> GetByIdAsync(string id);

    Task<Result<string>> CreateOrUpdateAsync(CreateOrUpdateRoleCommand command);

    Task<Result<string>> DeleteAsync(string id);
}