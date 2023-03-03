using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Application.Identity.Users.Commands.ConfirmEmail;
using MediatR;

namespace CheckflixApp.Application.Identity.Users.Commands.ConfirmPhoneNumber;

public class ConfirmPhoneNumerCommandHandler : IRequestHandler<ConfirmPhoneNumerCommand, string>
{
    private readonly IUserService _userService;

    public ConfirmPhoneNumerCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<string> Handle(ConfirmPhoneNumerCommand command, CancellationToken cancellationToken)
    {
        return await _userService.ConfirmPhoneNumberAsync(command.UserId, command.Code);
    }
}