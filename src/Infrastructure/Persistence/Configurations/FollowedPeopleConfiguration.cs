using CheckflixApp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CheckflixApp.Infrastructure.Identity;

namespace CheckflixApp.Infrastructure.Persistence.Configurations;


public class FollowedPeopleConfiguration : IEntityTypeConfiguration<FollowedPeople>
{
    public void Configure(EntityTypeBuilder<FollowedPeople> builder)
    {
        //https://stackoverflow.com/questions/20886049/ef-code-first-foreign-key-without-navigation-property
        builder.ToTable("FollowedPeople");
       
        builder.HasKey(fp => new { fp.ObserverId, fp.TargetId });

        builder.Ignore(x => x.Id);

        builder.Metadata
            .FindNavigation(nameof(ApplicationUser.Followers))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(ApplicationUser.Following))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasOne<ApplicationUser>()
            .WithMany(x => x.Following)
            .HasForeignKey(x => x.ObserverId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<ApplicationUser>()
            .WithMany(x => x.Followers)
            .HasForeignKey(x => x.TargetId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
