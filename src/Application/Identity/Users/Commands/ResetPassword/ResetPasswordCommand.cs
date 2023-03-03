﻿using MediatR;

namespace CheckflixApp.Application.Identity.Users.Commands.ResetPassword;
public class ResetPasswordCommand : IRequest<string>
{
    public string Email { get; set; } = default!;

    public string Password { get; set; } = default!;

    public string Token { get; set; } = default!;
}