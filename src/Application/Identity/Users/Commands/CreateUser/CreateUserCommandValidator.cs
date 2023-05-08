using FluentValidation;

namespace CheckflixApp.Application.Identity.Users.Commands.CreateUser;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserCommandValidator"/> class.
    /// </summary>
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty();

        RuleFor(x => x.Password).NotEmpty();

        RuleFor(x => x.ConfirmPassword).NotEmpty();

        RuleFor(x => x.UserName).NotEmpty();
    }
}