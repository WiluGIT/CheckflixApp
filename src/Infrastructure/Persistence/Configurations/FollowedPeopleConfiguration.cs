using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckflixApp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CheckflixApp.Infrastructure.Identity;
using System.Reflection.Emit;

namespace CheckflixApp.Infrastructure.Persistence.Configurations;


public class FollowedPeopleConfiguration : IEntityTypeConfiguration<FollowedPeople>
{
    public void Configure(EntityTypeBuilder<FollowedPeople> builder)
    {
        //https://stackoverflow.com/questions/20886049/ef-code-first-foreign-key-without-navigation-property
        builder.ToTable("FollowedPeople");
       
        builder.HasKey(fp => new { fp.ObserverId, fp.TargetId });

        builder.Ignore(x => x.Id);

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
