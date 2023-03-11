using CheckflixApp.Domain.Entities;
using CheckflixApp.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CheckflixApp.Infrastructure.Persistence.Configurations;

public class ProductionGenreConfiguration : IEntityTypeConfiguration<ProductionGenre>
{
    public void Configure(EntityTypeBuilder<ProductionGenre> builder)
    {
        builder.ToTable("ProductionGenres");

        builder.HasKey(fp => new { fp.ProductionId, fp.GenreId });

        builder.Ignore(x => x.Id);

        builder.Metadata
            .FindNavigation(nameof(Production.ProductionGenres))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(Genre.ProductionGenres))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder
            .HasOne<Production>()
            .WithMany(x => x.ProductionGenres)
            .HasForeignKey(x => x.ProductionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<Genre>()
            .WithMany(x => x.ProductionGenres)
            .HasForeignKey(x => x.GenreId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
