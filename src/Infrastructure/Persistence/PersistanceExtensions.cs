﻿using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Infrastructure.Persistence.Interceptors;
using CheckflixApp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CheckflixApp.Infrastructure.Persistence;
internal static class PersistanceExtensions
{
    internal static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("CheckflixAppDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddScoped<IFollowedPeopleRepository, FollowedPeopleRepository>();

        services.AddScoped<IProductionRepository, ProductionRepository>();

        services.AddScoped<IGenreRepository, GenreRepository>();

        services.AddScoped<IApplicationUserProductionRepository, ApplicationUserProductionRepository>();

        return services;
    }
}
