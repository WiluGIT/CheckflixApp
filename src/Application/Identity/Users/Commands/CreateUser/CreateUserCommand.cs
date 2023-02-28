using MediatR;

namespace CheckflixApp.Application.Identity.Users.Commands.CreateUser;
public class CreateUserCommand : IRequest<string>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
    public string? PhoneNumber { get; set; }
}