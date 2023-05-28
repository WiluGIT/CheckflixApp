using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckflixApp.Application.Common.Models;
using CheckflixApp.Domain.Entities;

namespace CheckflixApp.Application.Common.Interfaces;

/// <summary>
/// Represents the ApplicationUserProduction repository interface.
/// </summary>
public interface IApplicationUserProductionRepository
{
    /// <summary>
    /// Gets application user productions.
    /// </summary>
    /// <returns>The collection of system application user productions.</returns>
    Task<List<Production>> GetUserProductions(string userId);

    /// <summary>
    /// Inserts the specified ApplicationUserProduction to the database.
    /// </summary>
    /// <param name="production">The ApplicationUserProduction to be inserted into the database.</param>
    void Insert(ApplicationUserProduction applicationUserProduction);


    /// <summary>
    /// Removes the specified ApplicationUserProduction from the database.
    /// </summary>
    /// <param name="production">The ApplicationUserProduction to be removed from the database.</param>
    void Remove(ApplicationUserProduction applicationUserProduction);

    /// <summary>
    /// Updates the specified ApplicationUserProduction in the database.
    /// </summary>
    /// <param name="production">The ApplicationUserProduction to be updated in the database.</param>
    void Update(ApplicationUserProduction applicationUserProduction);

    /// <summary>
    /// Gets the ApplicationUserProduction with the specified identifier.
    /// </summary>
    /// <param name="applicationUserProductionId">The ApplicationUserProduction identifier.</param>
    /// <returns>The ApplicationUserProduction with specified identifier or null if does not exists.</returns>
    Task<ApplicationUserProduction?> GetByIdAsync(string userId, int productionId);
}