namespace CheckflixApp.Application.Followings.Common;
public class UserWithFollowingDto
{
    public string Id { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public bool IsFollowing { get; set; }
}
