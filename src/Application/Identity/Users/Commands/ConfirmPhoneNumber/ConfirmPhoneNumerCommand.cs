using MediatR;

namespace CheckflixApp.Application.Identity.Users.Commands.ConfirmPhoneNumber;

public class ConfirmPhoneNumerCommand : IRequest<string>
{
    public string UserId { get; set; } = default!;
    public string Code { get; set; } = default!;
}