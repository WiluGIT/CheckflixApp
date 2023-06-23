using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Common.Mappings;
using CheckflixApp.Application.Common.Models;
using CheckflixApp.Application.Common.Specification;
using CheckflixApp.Application.Productions.Common;
using CheckflixApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheckflixApp.Infrastructure.Persistence.Repositories;

/// <summary>
/// Represents the Production repository.
/// </summary>
internal sealed class ProductionRepository : GenericRepository<Production>, IProductionRepository
{
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductionRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public ProductionRepository(IApplicationDbContext dbContext, IMapper mapper)
        : base(dbContext)
    {
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProductionDto>> GetAllProductions(PaginationFilter filter) =>
        await DbContext.Set<Production>()
            .AsNoTracking()
            .Include(x => x.ProductionGenres)
            .WithSpecification(new EntitiesByBaseFilterSpec<Production>(filter))
            .Select(x => new ProductionDto
            {
                ProductionId = x.Id,
                Director = x.Director,
                ReleaseDate = x.ReleaseDate,
                ImdbId = x.ImdbId,
                Keywords = x.Keywords,
                Overview = x.Overview,
                Title = x.Title,
                TmdbId = x.TmdbId,
                Genres = (from productionGenre in x.ProductionGenres
                          join genre in DbContext.Set<Genre>() on productionGenre.GenreId equals genre.Id
                          select genre.Name).ToArray()
            }).PaginatedListAsync(filter.PageNumber, filter.PageSize);



    public new async Task<Production?> GetByIdAsync(int id) => 
        await DbContext.Set<Production>()
            .Include(x => x.ProductionGenres)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

    public async Task<ProductionDto?> GetProductionDtoById(int productionId) =>
        await (from p in DbContext.Set<Production>()
               join pg in DbContext.Set<ProductionGenre>() on p.Id equals pg.ProductionId into ppg
               from pg in ppg.DefaultIfEmpty()
               join g in DbContext.Set<Genre>() on pg.GenreId equals g.Id into gpg
               from g in gpg.DefaultIfEmpty()
               where p.Id == productionId
               group g by new { p.Id, p.Director, p.ReleaseDate, p.ImdbId, p.Keywords, p.Overview, p.Title, p.TmdbId } into gGroup
               select new ProductionDto
               {
                   ProductionId = gGroup.Key.Id,
                   Director = gGroup.Key.Director,
                   ReleaseDate = gGroup.Key.ReleaseDate,
                   ImdbId = gGroup.Key.ImdbId,
                   Keywords = gGroup.Key.Keywords,
                   Overview = gGroup.Key.Overview,
                   Title = gGroup.Key.Title,
                   TmdbId = gGroup.Key.TmdbId,
                   Genres = gGroup.Select(g => g.Name).ToArray()
               }).FirstOrDefaultAsync();
}