using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Identity.Users.Commands.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<string>>
{
    private readonly IUserService _userService;

    public ResetPasswordCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result<string>> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        return await _userService.ResetPasswordAsync(command);
    }
}