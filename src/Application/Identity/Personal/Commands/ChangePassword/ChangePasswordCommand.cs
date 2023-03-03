using CheckflixApp.Application.Common.FileStorage;
using CheckflixApp.Application.Common.Security;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.Identity.Personal.Commands.ChangePassword;

[Authorize]
public class ChangePasswordCommand : IRequest<string>
{
    public string Password { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
    public string ConfirmNewPassword { get; set; } = default!;
}

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordRequestValidator(IStringLocalizer<ChangePasswordRequestValidator> T)
    {
        RuleFor(p => p.Password)
            .NotEmpty();

        RuleFor(p => p.NewPassword)
            .NotEmpty();

        RuleFor(p => p.ConfirmNewPassword)
            .Equal(p => p.NewPassword)
                .WithMessage(T["Passwords do not match."]);
    }
}