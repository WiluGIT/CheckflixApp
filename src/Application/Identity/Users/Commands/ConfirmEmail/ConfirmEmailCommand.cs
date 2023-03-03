using MediatR;

namespace CheckflixApp.Application.Identity.Users.Commands.ConfirmEmail;

public class ConfirmEmailCommand : IRequest<string>
{
    public string UserId { get; set; } = default!;
    public string Code { get; set; } = default!;
}