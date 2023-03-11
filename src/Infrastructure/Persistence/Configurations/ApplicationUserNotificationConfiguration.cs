using CheckflixApp.Domain.Entities;
using CheckflixApp.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CheckflixApp.Infrastructure.Persistence.Configurations;
public class ApplicationUserNotificationConfiguration : IEntityTypeConfiguration<ApplicationUserNotification>
{
    public void Configure(EntityTypeBuilder<ApplicationUserNotification> builder)
    {
        builder.ToTable("ApplicationUserNotifications");

        builder.HasKey(fp => new { fp.ApplicationUserId, fp.NotificationId });

        builder.Ignore(x => x.Id);

        builder.Metadata
            .FindNavigation(nameof(ApplicationUser.ApplicationUserNotifications))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(Notification.ApplicationUserNotifications))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder
            .HasOne<ApplicationUser>()
            .WithMany(x => x.ApplicationUserNotifications)
            .HasForeignKey(x => x.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<Notification>()
            .WithMany(x => x.ApplicationUserNotifications)
            .HasForeignKey(x => x.NotificationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
