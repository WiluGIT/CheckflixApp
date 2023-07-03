using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Common.Models;
using CheckflixApp.Application.Common.Specification;
using CheckflixApp.Application.Genres.Common;
using CheckflixApp.Application.Productions.Common;
using CheckflixApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheckflixApp.Infrastructure.Persistence.Repositories;

/// <summary>
/// Represents the Genre repository.
/// </summary>
internal sealed class GenreRepository : GenericRepository<Genre>, IGenreRepository
{
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenreRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public GenreRepository(IApplicationDbContext dbContext, IMapper mapper)
        : base(dbContext)
    {
        _mapper = mapper;
    }

    public async Task<List<GenreDto>> GetAllGenres()
        => await DbContext.Set<Genre>()
            .ProjectTo<GenreDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

    public async Task<bool> ValidateIfGenresExists(List<int> genreIds)
    {
        var existingGenres = await DbContext.Set<Genre>()
            .Where(x => genreIds.Contains(x.Id))
            .ToListAsync();

        return genreIds.Count == existingGenres.Count;
    }

    public async Task<List<ProductionDto>> GetGenresProductions(GenresFilter filter)
    {
        var productions = await (
            from p in DbContext.Set<Production>()
            join pg in DbContext.Set<ProductionGenre>() on p.Id equals pg.ProductionId
            join g in DbContext.Set<Genre>() on pg.GenreId equals g.Id
            where filter.GenreIds.Contains(g.Id)
            group new { p, g, pg } by p into grouped
            select new ProductionDto
            {
                ProductionId = grouped.Key.Id,
                Title = grouped.Key.Title,
                TmdbId = grouped.Key.TmdbId,
                ImdbId = grouped.Key.ImdbId,
                Overview = grouped.Key.Overview,
                Director = grouped.Key.Director,
                Keywords = grouped.Key.Keywords,
                ReleaseDate = grouped.Key.ReleaseDate,
                Genres = grouped.Select(pg => pg.g.Name).ToList()
            }
        ).ToListAsync();

        //var productions = await DbContext.Set<Production>()
        //    .Join(
        //        DbContext.Set<ProductionGenre>(),
        //        p => p.Id,
        //        pg => pg.ProductionId,
        //        (p, pg) => new { Production = p, ProductionGenre = pg }
        //    )
        //    .Join(
        //        DbContext.Set<Genre>(),
        //        pg => pg.ProductionGenre.GenreId,
        //        g => g.Id,
        //        (pg, g) => new { pg.Production, Genre = g }
        //    )
        //    .Where(pg => filter.GenreIds.Contains(pg.Genre.Id))
        //    .GroupBy(pg => pg.Production.Id)
        //    .Select(g => new ProductionDto
        //    {
        //        ProductionId = g.Key,
        //        Title = g.First().Production.Title,
        //        Genres = g.Select(pg => pg.Genre.Name).ToList()
        //    })
        //.ToListAsync();

        return productions;
    }
}