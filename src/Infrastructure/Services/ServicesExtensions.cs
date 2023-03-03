using CheckflixApp.Application.Auditing.Interfaces;
using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Infrastructure.Auditing;
using CheckflixApp.Infrastructure.Files;
using CheckflixApp.Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CheckflixApp.Infrastructure.Services;
internal static class ServicesExtensions
{
    internal static IServiceCollection AddInternalServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IAuditService, AuditService>();
        services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

        return services;
    }
}
