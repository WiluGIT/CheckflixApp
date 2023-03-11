using CheckflixApp.Domain.Entities;
using CheckflixApp.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CheckflixApp.Infrastructure.Persistence.Configurations;

public class ApplicationUserGenreConfiguration : IEntityTypeConfiguration<ApplicationUserGenre>
{
    public void Configure(EntityTypeBuilder<ApplicationUserGenre> builder)
    {
        builder.ToTable("ApplicationUserGenres");

        builder.HasKey(fp => new { fp.ApplicationUserId, fp.GenreId });

        builder.Ignore(x => x.Id);

        builder.Metadata
            .FindNavigation(nameof(ApplicationUser.ApplicationUserGenres))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(Genre.ApplicationUserGenres))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder
            .HasOne<ApplicationUser>()
            .WithMany(x => x.ApplicationUserGenres)
            .HasForeignKey(x => x.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<Genre>()
            .WithMany(x => x.ApplicationUserGenres)
            .HasForeignKey(x => x.GenreId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
