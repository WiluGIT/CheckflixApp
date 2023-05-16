using AutoMapper;
using AutoMapper.QueryableExtensions;
using CheckflixApp.Application.Common.Exceptions;
using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Common.Mappings;
using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Application.Identity.Roles.Commands.CreateOrUpdateRole;
using CheckflixApp.Domain.Common.Primitives;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Infrastructure.Identity;

internal class RoleService : IRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _db;
    private readonly IStringLocalizer _t;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    // TODO: Create event publisher and check domain events
    //private readonly IEventPublisher _events;

    public RoleService(
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext db,
        IStringLocalizer<RoleService> localizer,
        ICurrentUserService currentUserService
,
        IMapper mapper
        //IEventPublisher events
        )
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _db = db;
        _t = localizer;
        _currentUserService = currentUserService;
        _mapper = mapper;
        //_events = events;
    }

    public async Task<List<RoleDto>> GetListAsync(CancellationToken cancellationToken)
        => await _roleManager.Roles.ProjectToListAsync<RoleDto>(_mapper.ConfigurationProvider);

    public async Task<int> GetCountAsync(CancellationToken cancellationToken) 
        => await _roleManager.Roles.CountAsync(cancellationToken);

    public async Task<bool> ExistsAsync(string roleName, string? excludeId) 
        => await _roleManager.FindByNameAsync(roleName) is IdentityRole existingRole && existingRole.Id != excludeId;

    public async Task<Result<RoleDto>> GetByIdAsync(string id)
        => await _db.Roles.SingleOrDefaultAsync(x => x.Id == id) is { } role ? _mapper.Map<RoleDto>(role)
            : Error.NotFound(description: _t["Role Not Found"]);

    public async Task<Result<string>> CreateOrUpdateAsync(CreateOrUpdateRoleCommand command)
    {
        if (string.IsNullOrEmpty(command.Id))
        {
            // Create a new role.
            var newRole = new IdentityRole(command.Name);
            var createResult = await _roleManager.CreateAsync(newRole);

            if (!createResult.Succeeded)
            {
                return Error.Failure(description: string.Join(',', _t["Register role failed"], createResult.GetErrors(_t)));
            }

            //await _events.PublishAsync(new ApplicationRoleCreatedEvent(role.Id, role.Name));

            return string.Format(_t["Role {0} Created."], command.Name);
        }

        // Update an existing role.
        IdentityRole? role = await _roleManager.FindByIdAsync(command.Id);

        if (role == null)
        {
            return Error.NotFound(description: _t["Role Not Found"]);
        }

        if (SystemRoles.IsDefault(role.Name))
        {
            return Error.Failure(description: string.Format(_t["Not allowed to modify {0} Role."], role.Name));
        }

        role.Name = command.Name;
        role.NormalizedName = command.Name.ToUpperInvariant();
        var result = await _roleManager.UpdateAsync(role);

        if (!result.Succeeded)
        {
            return Error.Failure(description: string.Join(',',_t["Update role failed"], result.GetErrors(_t)));
        }

        //await _events.PublishAsync(new ApplicationRoleUpdatedEvent(role.Id, role.Name));

        return string.Format(_t["Role {0} Updated."], role.Name);
    }

    public async Task<Result<string>> DeleteAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);

        if (role == null)
        {
            return Error.NotFound(description: _t["Role Not Found"]);
        }

        if (SystemRoles.IsDefault(role.Name))
        {
            return Error.Failure(description: string.Format(_t["Not allowed to delete {0} Role."], role.Name));
        }

        if ((await _userManager.GetUsersInRoleAsync(role.Name)).Count > 0)
        {
            return Error.Failure(description: string.Format(_t["Not allowed to delete {0} Role as it is being used."], role.Name));
        }

        await _roleManager.DeleteAsync(role);

        //await _events.PublishAsync(new ApplicationRoleDeletedEvent(role.Id, role.Name));

        return string.Format(_t["Role {0} Deleted."], role.Name);
    }
}