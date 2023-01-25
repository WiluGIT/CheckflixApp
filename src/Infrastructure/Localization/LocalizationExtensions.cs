using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Infrastructure.Localization;
internal static class LocalizationExtensions
{
    internal static IServiceCollection AddInternalLocalization(this IServiceCollection services, IConfiguration config)
    {
        services.AddLocalization();
        services.AddSingleton<LocalizationMiddleware>();
        services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

        return services;
    }

    internal static IApplicationBuilder UseLocalization(this IApplicationBuilder app, IConfiguration config)
    {
        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(new CultureInfo("en-US"))
        })
        .UseMiddleware<LocalizationMiddleware>();

        return app;
    }
}
