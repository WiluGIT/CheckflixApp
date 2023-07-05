using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckflixApp.Application.Common.Models;
using CheckflixApp.Application.Genres.Common;
using CheckflixApp.Application.Productions.Common;
using CheckflixApp.Domain.Entities;

namespace CheckflixApp.Application.Common.Interfaces;

/// <summary>
/// Represents the Genre repository interface.
/// </summary>
public interface IGenreRepository
{
    /// <summary>
    /// Validates if genres from specified collection exists in database.
    /// </summary>
    /// <returns>The validation result.</returns>
    Task<bool> ValidateIfGenresExists(List<int> genreIds);

    /// <summary>
    /// Gets all production genres.
    /// </summary>
    /// <returns>The collection of production genres.</returns>
    Task<List<GenreDto>> GetAllGenres();

    /// <summary>
    /// Gets all productions with specified genres.
    /// </summary>
    /// <returns>The collection of productions with genres.</returns>
    Task<PaginatedList<ProductionDto>> GetGenresProductions(GenresFilter filter);
}