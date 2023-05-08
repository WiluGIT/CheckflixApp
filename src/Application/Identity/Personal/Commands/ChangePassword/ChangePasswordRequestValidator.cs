﻿using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.Identity.Personal.Commands.ChangePassword;

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