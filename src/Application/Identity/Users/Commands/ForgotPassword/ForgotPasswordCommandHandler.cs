using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CheckflixApp.Application.Identity.Users.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result<string>>
{
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _contextAccessor;

    public ForgotPasswordCommandHandler(IUserService userService, IHttpContextAccessor contextAccessor)
    {
        _userService = userService;
        _contextAccessor = contextAccessor;
    }

    public async Task<Result<string>> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
    {

        return await _userService.ForgotPasswordAsync(command, GetOriginFromRequest());
    }

    private string GetOriginFromRequest() => 
        $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host.Value}{_contextAccessor.HttpContext.Request.PathBase.Value}";
}