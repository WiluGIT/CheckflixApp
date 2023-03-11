namespace CheckflixApp.Domain.Entities;
public class FollowedPeople : BaseAuditableEntity
{
    public string ObserverId { get; set; }
    public string TargetId { get; set; }


    private FollowedPeople(
        string observerId,
        string targetId)
    {
        this.ObserverId = observerId;
        this.TargetId = targetId;
    }

    public static FollowedPeople Create(
        string observerId,
        string targetId)
    {
        return new(
            observerId: observerId, 
            targetId: targetId);
    }

    #pragma warning disable CS8618 // Required by Entity Framework
    private FollowedPeople() { }
}