namespace CheckflixApp.Domain.Entities;
public class Notification : BaseAuditableEntity
{
    public DateTime Date { get; set; }
    public string Content { get; set; }


    private readonly List<ApplicationUserNotification> _applicationUserNotifications = new();
    public IReadOnlyList<ApplicationUserNotification> ApplicationUserNotifications => _applicationUserNotifications.AsReadOnly();

    private Notification(
        DateTime date,
        string content)
    {
        this.Date = date;
        this.Content = content;
    }

    public static Notification Create(
        DateTime date,
        string content)
    {
        return new(
            date: date, 
            content: content);
    }

    #pragma warning disable CS8618 // Required by Entity Framework
    private Notification() { }
}