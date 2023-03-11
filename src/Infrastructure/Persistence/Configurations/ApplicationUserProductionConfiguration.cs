using CheckflixApp.Domain.Entities;
using CheckflixApp.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CheckflixApp.Infrastructure.Persistence.Configurations;

public class ApplicationUserProductionConfiguration : IEntityTypeConfiguration<ApplicationUserProduction>
{
    public void Configure(EntityTypeBuilder<ApplicationUserProduction> builder)
    {
        builder.ToTable("ApplicationUserProductions");

        builder.HasKey(fp => new { fp.ApplicationUserId, fp.ProductionId });

        builder.Ignore(x => x.Id);

        builder.Metadata
            .FindNavigation(nameof(ApplicationUser.ApplicationUserProductions))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(Production.ApplicationUserProductions))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder
            .HasOne<ApplicationUser>()
            .WithMany(x => x.ApplicationUserProductions)
            .HasForeignKey(x => x.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<Production>()
            .WithMany(x => x.ApplicationUserProductions)
            .HasForeignKey(x => x.ProductionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
