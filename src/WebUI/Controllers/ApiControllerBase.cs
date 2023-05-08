using CheckflixApp.Domain.Common.Errors;
using CheckflixApp.Domain.Common.Primitives;
using MediatR;

using Microsoft.AspNetCore.Mvc;
using WebUI.Common;

namespace CheckflixApp.WebUI.Controllers;
[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    ///// <summary>
    ///// Creates an <see cref="BadRequestObjectResult"/> that produces a <see cref="StatusCodes.Status400BadRequest"/>.
    ///// response based on the specified <see cref="Result"/>.
    ///// </summary>
    ///// <param name="error">The error.</param>
    ///// <returns>The created <see cref="BadRequestObjectResult"/> for the response.</returns>
    //protected IActionResult BadRequest(IEnumerable<Domain.Common.Primitives.Error> errors) => BadRequest(new ApiErrorResponse(errors));

    ///// <summary>
    ///// Creates an <see cref="OkObjectResult"/> that produces a <see cref="StatusCodes.Status200OK"/>.
    ///// </summary>
    ///// <returns>The created <see cref="OkObjectResult"/> for the response.</returns>
    ///// <returns></returns>
    //protected new IActionResult Ok(object value) => base.Ok(value);

    ///// <summary>
    ///// Creates an <see cref="NotFoundResult"/> that produces a <see cref="StatusCodes.Status404NotFound"/>.
    ///// </summary>
    ///// <returns>The created <see cref="NotFoundResult"/> for the response.</returns>
    //protected new IActionResult NotFound() => base.NotFound();

    protected IActionResult Problem(List<Error> errors)
    {
        HttpContext.Items[CommonKeys.Errors] = errors;
        var firstError = errors[0];

        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: firstError.Message);
    }
}
