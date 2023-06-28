using Microsoft.AspNetCore.Http;

namespace CheckflixApp.Application.Common.Extensions;
public static class HttpExtensions
{
    public static string GetBaseUrl(this HttpContext httpContext) =>
        $"{httpContext.Request.Scheme}://{httpContext.Request.Host.Value}{httpContext.Request.PathBase.Value}";
}