using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification.EntityFrameworkCore;
using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Common.Mappings;
using CheckflixApp.Application.Common.Models;
using CheckflixApp.Application.Common.Specification;
using CheckflixApp.Domain.Entities;
using CheckflixApp.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace CheckflixApp.Infrastructure.Persistence.Repositories;

/// <summary>
/// Represents the Production repository.
/// </summary>
internal sealed class ProductionRepository : GenericRepository<Production>, IProductionRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductionRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public ProductionRepository(IApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<PaginatedList<Production>> GetAllProductions(PaginationFilter filter) =>
        await DbContext.Set<Production>()
            .WithSpecification(new EntitiesByPaginationFilterSpec<Production>(filter))
            .PaginatedListAsync(filter.PageNumber, filter.PageSize);
}