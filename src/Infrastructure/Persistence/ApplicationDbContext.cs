using System.Reflection;
using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Domain.Common;
using CheckflixApp.Domain.Common.Primitives.Maybe;
using CheckflixApp.Domain.Entities;
using CheckflixApp.Infrastructure.Identity;
using CheckflixApp.Infrastructure.Persistence.Interceptors;
using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;

namespace CheckflixApp.Infrastructure.Persistence;
public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext, IUnitOfWork
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options, operationalStoreOptions)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<TodoList> TodoLists => Set<TodoList>();
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<FollowedPeople> FollowedPeople => Set<FollowedPeople>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        => Database.BeginTransactionAsync(cancellationToken);

    /// <inheritdoc />
    public new DbSet<TEntity> Set<TEntity>()
        where TEntity : BaseEntity
        => base.Set<TEntity>();

    /// <inheritdoc />
    public async Task<TEntity?> GetBydIdAsync<TEntity>(int id)
        where TEntity : BaseEntity
        => await Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);

    /// <inheritdoc />
    public void Insert<TEntity>(TEntity entity)
        where TEntity : BaseEntity
        => Set<TEntity>().Add(entity);

    /// <inheritdoc />
    public void InsertRange<TEntity>(IReadOnlyCollection<TEntity> entities)
        where TEntity : BaseEntity
        => Set<TEntity>().AddRange(entities);

    /// <inheritdoc />
    public new void Remove<TEntity>(TEntity entity)
        where TEntity : BaseEntity
        => Set<TEntity>().Remove(entity);

    /// <inheritdoc />
    //public Task<int> ExecuteSqlAsync(string sql, IEnumerable<SqlParameter> parameters, CancellationToken cancellationToken = default)
    //    => Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);
}
