using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Application.Identity.Users.Commands.ConfirmPhoneNumber;
using MediatR;

namespace CheckflixApp.Application.Identity.Users.Commands.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, string>
{
    private readonly IUserService _userService;

    public ResetPasswordCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<string> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        return await _userService.ResetPasswordAsync(command);
    }
}