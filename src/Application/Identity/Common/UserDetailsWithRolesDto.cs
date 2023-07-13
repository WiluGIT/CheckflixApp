namespace CheckflixApp.Application.Identity.Common;
public class UserDetailsWithRolesDto : UserDetailsDto
{
    public IList<string> Roles { get; set; } = new List<string>();
}


