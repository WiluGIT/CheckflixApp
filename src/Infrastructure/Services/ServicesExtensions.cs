using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Identity.Tokens.Interfaces;
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
        services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

        return services;
    }
}
