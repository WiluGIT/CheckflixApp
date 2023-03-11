namespace CheckflixApp.Domain.Entities;
public class ApplicationUserNotification : BaseAuditableEntity
{
    public string ApplicationUserId { get; set; }
    public int NotificationId { get; set; }


    private ApplicationUserNotification(
        string applicationUserId,
        int notificationId)
    {
        this.ApplicationUserId = applicationUserId;
        this.NotificationId = notificationId;
    }

    public static ApplicationUserNotification Create(
        string applicationUserId,
        int notificationId)
    {
        return new(
            applicationUserId: applicationUserId,
            notificationId: notificationId);
    }

    #pragma warning disable CS8618 // Required by Entity Framework
    private ApplicationUserNotification() { }
}