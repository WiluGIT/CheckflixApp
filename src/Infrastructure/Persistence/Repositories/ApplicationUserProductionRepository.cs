using CheckflixApp.Application.ApplicationUserProductions.Common;
using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheckflixApp.Infrastructure.Persistence.Repositories;

/// <summary>
/// Represents the ApplicationUserProduction repository.
/// </summary>
internal sealed class ApplicationUserProductionRepository : GenericRepository<ApplicationUserProduction>, IApplicationUserProductionRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationUserProductionRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public ApplicationUserProductionRepository(IApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<ApplicationUserProduction?> GetByIdAsync(string userId, int productionId) =>
        await DbContext.Set<ApplicationUserProduction>()
        .FirstOrDefaultAsync(x => x.ApplicationUserId.Equals(userId) && x.ProductionId.Equals(productionId));

    public async Task<UserCollectionsDto> GetUserProductions(string userId)
        => new UserCollectionsDto
        {
            Favourites = await (from p in DbContext.Set<Production>()
                                join up in DbContext.Set<ApplicationUserProduction>() on p.Id equals up.ProductionId
                                where up.ApplicationUserId == userId && up.Favourites == true
                                select new ProductionBasicDto
                                {
                                    ProductionId = p.Id,
                                    ReleaseDate = p.ReleaseDate,
                                    ImdbId = p.ImdbId,
                                    Title = p.Title,
                                    TmdbId = p.TmdbId
                                }).ToListAsync(),
            ToWatch = await (from p in DbContext.Set<Production>()
                             join up in DbContext.Set<ApplicationUserProduction>() on p.Id equals up.ProductionId
                             where up.ApplicationUserId == userId && up.ToWatch == true
                             select new ProductionBasicDto
                             {
                                 ProductionId = p.Id,
                                 ReleaseDate = p.ReleaseDate,
                                 ImdbId = p.ImdbId,
                                 Title = p.Title,
                                 TmdbId = p.TmdbId
                             }).ToListAsync(),
            Watched = await (from p in DbContext.Set<Production>()
                             join up in DbContext.Set<ApplicationUserProduction>() on p.Id equals up.ProductionId
                             where up.ApplicationUserId == userId && up.Watched == true
                             select new ProductionBasicDto
                             {
                                 ProductionId = p.Id,
                                 ReleaseDate = p.ReleaseDate,
                                 ImdbId = p.ImdbId,
                                 Title = p.Title,
                                 TmdbId = p.TmdbId
                             }).ToListAsync(),
        };
}