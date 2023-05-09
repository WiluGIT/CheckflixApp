using FluentValidation;

namespace CheckflixApp.Application.Identity.Users.Commands.ForgotPassword;

public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator() =>
        RuleFor(p => p.Email).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress()
                .WithMessage("Invalid Email Address.");
}