using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Identity.Tokens.Interfaces;
using CheckflixApp.Infrastructure.Auth;
using CheckflixApp.Infrastructure.Files;
using CheckflixApp.Infrastructure.Identity;
using CheckflixApp.Infrastructure.Localization;
using CheckflixApp.Infrastructure.Persistence;
using CheckflixApp.Infrastructure.Persistence.Interceptors;
using CheckflixApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

namespace Microsoft.Extensions.DependencyInjection;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
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

        services.AddScoped<ApplicationDbContextInitialiser>();

        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

        services.AddAuthentication()
            .AddIdentityServerJwt();

        services.AddAuthorization(options =>
            options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

        //to move
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        services.Configure<SecuritySettings>(configuration.GetSection(nameof(SecuritySettings)));
        services.AddLocalization();
        services.AddSingleton<LocalizationMiddleware>();
        services.AddDistributedMemoryCache();
        services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

        return services;
    }
}
