using CheckflixApp.Application.Common.Mappings;
using Microsoft.AspNetCore.Identity;

namespace CheckflixApp.Application.Identity.Common;

// Added Microsoft.Extensions.Identity.Stores to Create this Map in Application Layer
public class RoleDto : IMapFrom<IdentityRole>
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public List<string>? Permissions { get; set; }
}