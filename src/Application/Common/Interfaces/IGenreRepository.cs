using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckflixApp.Application.Common.Models;
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
}