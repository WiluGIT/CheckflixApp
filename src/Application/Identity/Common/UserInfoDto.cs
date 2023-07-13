using CheckflixApp.Application.Common.Mappings;
using Microsoft.AspNetCore.Identity;

namespace CheckflixApp.Application.Identity.Common;

public class UserInfoDto : IMapFrom<IdentityUser>
{
    public string Id { get; set; } = string.Empty;
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? AccessToken { get; set; }
    public IList<string> Roles { get; set; } = new List<string>();
}
