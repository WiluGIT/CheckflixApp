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

    public async Task<List<Production>> GetUserProductions(string userId)
    {
        //var applicationUserProductions = await DbContext.Set<ApplicationUserProduction>()
        //    .Where(x => x.ApplicationUserId.Equals(userId))
        //    .ToListAsync();

        var productions = await (from applicationUserProduction in DbContext.Set<ApplicationUserProduction>().AsNoTracking()
                                 join production in DbContext.Set<Production>().AsNoTracking()
                                    on applicationUserProduction.ProductionId equals production.Id
                                 where applicationUserProduction.ApplicationUserId.Equals(userId)
                                 select production).ToListAsync();

        return productions;
    }

    public async Task<ApplicationUserProduction?> GetByIdAsync(string userId, int productionId) =>
        await DbContext.Set<ApplicationUserProduction>()
        .FirstOrDefaultAsync(x => x.ApplicationUserId.Equals(userId) && x.ProductionId.Equals(productionId));
}