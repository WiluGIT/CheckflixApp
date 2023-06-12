using CheckflixApp.Infrastructure.Auth;
using CheckflixApp.Infrastructure.BackgroundJobs;
using CheckflixApp.Infrastructure.Identity;
using CheckflixApp.Infrastructure.Localization;
using CheckflixApp.Infrastructure.Mailing;
using CheckflixApp.Infrastructure.Persistence;
using CheckflixApp.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddPersistance(configuration)
            .AddAuth(configuration)
            .AddBackgroundJobs(configuration)
            .AddInternalServices(configuration)
            .AddHttpClient(configuration)
            .AddConfigurations(configuration)
            .AddInternalLocalization(configuration)
            .AddDistributedMemoryCache();

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config)
        => builder
            .UseHealthChecks("/health")
            .UseHttpsRedirection()
            .UseStaticFiles()
            .UseLocalization(config)
            .UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            })
            .UseRouting()
            .UseCors("CorsApi")
            .UseAuthentication()
            .UseHangfireDashboard(config)
            .UseIdentityServer()
            .UseAuthorization();

    public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        services.Configure<SecuritySettings>(configuration.GetSection(nameof(SecuritySettings)));
        services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));

        return services;
    }
}
