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

    public async Task<PaginatedList<Production>> GetAllProductions(PaginationFilter filter) =>
        await DbContext.Set<Production>()
            .WithSpecification(new EntitiesByPaginationFilterSpec<Production>(filter))
            .PaginatedListAsync(filter.PageNumber, filter.PageSize);

    public new async Task<Production?> GetByIdAsync(int id) => 
        await DbContext.Set<Production>()
            .Include(x => x.ProductionGenres)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

    public async Task<ProductionDto?> GetProductionDtoById(int productionId)
    {
        var productionsWithGenres = await (from productionGenre in DbContext.Set<ProductionGenre>().AsNoTracking()
                          join genre in DbContext.Set<Genre>().AsNoTracking()
                               on productionGenre.GenreId equals genre.Id
                          join production in DbContext.Set<Production>().AsNoTracking()
                               on productionGenre.ProductionId equals production.Id
                          where productionGenre.ProductionId.Equals(productionId)
                          select new { Productions = production, Genres = genre })
                          .ToListAsync();

        var productionData = productionsWithGenres.First();

        var productionDto = new ProductionDto
        {
            Director = productionData.Productions.Title,
            ImdbId = productionData.Productions.ImdbId,
            Keywords = productionData.Productions.Keywords,
            Overview = productionData.Productions.Overview,
            Title = productionData.Productions.Title,
            TmdbId = productionData.Productions.TmdbId,        
            Genres = productionsWithGenres.Select(x => x.Genres.Name)
        };



        //var production = await DbContext.Set<Production>()
        //    .Include(x => x.ProductionGenres)
        //    .Where(x => x.Id.Equals(productionId))
        //    .ProjectTo<ProductionDto>(_mapper.ConfigurationProvider)
        //    .FirstOrDefaultAsync();

        var production2 = await GetByIdAsync(productionId);

        var productionGenreIds = production2.ProductionGenres.Select(x => x.GenreId);

        var genres = await  DbContext.Set<Genre>()
            .Where(x => productionGenreIds.Contains(x.Id))
            .Select(x => x.Name)
            .ToListAsync();

        var productionDto1= _mapper.Map<ProductionDto>(production2);
        productionDto.Genres = genres;

        return productionDto;
    }
        //await DbContext.Set<Production>()
        //    .Include(x => x.ProductionGenres)
        //    .Where(x => x.Id.Equals(productionId))
        //    .ProjectTo<ProductionDto>(_mapper.ConfigurationProvider)
        //    .FirstOrDefaultAsync();        
 
}