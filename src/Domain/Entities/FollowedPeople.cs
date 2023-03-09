namespace CheckflixApp.Domain.Entities;
public class FollowedPeople : BaseAuditableEntity
{
    public string ObserverId { get; set; }
    public string TargetId { get; set; }
}