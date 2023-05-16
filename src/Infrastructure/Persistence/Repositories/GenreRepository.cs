using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Common.Models;
using CheckflixApp.Application.Common.Specification;
using CheckflixApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheckflixApp.Infrastructure.Persistence.Repositories;

/// <summary>
/// Represents the Production repository.
/// </summary>
internal sealed class GenreRepository : GenericRepository<Genre>, IGenreRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductionRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public GenreRepository(IApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<bool> ValidateIfGenresExists(List<int> genreIds)
    {
        var existingGenres = await DbContext.Set<Genre>()
            .Where(x => genreIds.Contains(x.Id))
            .ToListAsync();

        return genreIds.Count == existingGenres.Count;
    }
}