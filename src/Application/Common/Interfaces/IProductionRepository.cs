using CheckflixApp.Application.Common.Models;
using CheckflixApp.Domain.Entities;

namespace CheckflixApp.Application.Common.Interfaces;

/// <summary>
/// Represents the Production repository interface.
/// </summary>
public interface IProductionRepository
{
    /// <summary>
    /// Gets paginated system productions.
    /// </summary>
    /// <returns>The IQueryable collection of system productions.</returns>
    Task<PaginatedList<Production>> GetAllProductions(PaginationFilter filter);

    /// <summary>
    /// Gets the production with the specified identifier.
    /// </summary>
    /// <param name="productionId">The production identifier.</param>
    /// <returns>The Production with specified identifier or null if does not exists.</returns>
    Task<Production?> GetByIdAsync(int productionId);

    /// <summary>
    /// Inserts the specified production to the database.
    /// </summary>
    /// <param name="production">The production to be inserted into the database.</param>
    void Insert(Production production);


    /// <summary>
    /// Removes the specified production from the database.
    /// </summary>
    /// <param name="production">The production to be removed from the database.</param>
    void Remove(Production production);
}