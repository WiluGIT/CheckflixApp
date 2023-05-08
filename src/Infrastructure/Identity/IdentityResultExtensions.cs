using System.Collections.Generic;
using CheckflixApp.Domain.Common.Primitives;
using CheckflixApp.Domain.Common.Primitives.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Infrastructure.Identity;
public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.From()
            : result.Errors.Select(e => new Error(e.Code, e.Description, ErrorType.Validation)).ToList();
    }

    public static List<string> GetErrors(this IdentityResult result, IStringLocalizer localizer) =>
        result.Errors.Select(e => localizer[e.Description].ToString()).ToList();
}
