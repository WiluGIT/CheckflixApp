﻿using CheckflixApp.Domain.Common;
using CheckflixApp.Domain.Common.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;

namespace CheckflixApp.Application.Common.Interfaces;

/// <summary>
/// Represents the application database context interface.
/// </summary>
public interface IApplicationDbContext
{
    /// <summary>
    /// Gets the database set for the specified entity type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <returns>The database set for the specified entity type.</returns>
    DbSet<TEntity> Set<TEntity>()
        where TEntity : BaseEntity;

    /// <summary>
    /// Gets the entity with the specified identifier.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <param name="id">The entity identifier.</param>
    /// <returns>The <typeparamref name="TEntity"/> with the specified identifier if it exists, otherwise null.</returns>
    Task<TEntity?> GetBydIdAsync<TEntity>(int id)
        where TEntity : BaseEntity;

    /// <summary>
    /// Inserts the specified entity into the database.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <param name="entity">The entity to be inserted into the database.</param>
    void Insert<TEntity>(TEntity entity)
        where TEntity : BaseEntity;

    /// <summary>
    /// Inserts the specified entities into the database.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <param name="entities">The entities to be inserted into the database.</param>
    void InsertRange<TEntity>(IReadOnlyCollection<TEntity> entities)
        where TEntity : BaseEntity;

    /// <summary>
    /// Removes the specified entity from the database.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <param name="entity">The entity to be removed from the database.</param>
    void Remove<TEntity>(TEntity entity)
        where TEntity : BaseEntity;

    /// <summary>
    /// Executes the specified SQL command asynchronously and returns the number of affected rows.
    /// </summary>
    /// <param name="sql">The SQL command.</param>
    /// <param name="parameters">The parameters collection.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of rows affected.</returns>
    //Task<int> ExecuteSqlAsync(string sql, IEnumerable<SqlParameter> parameters, CancellationToken cancellationToken = default);
}
