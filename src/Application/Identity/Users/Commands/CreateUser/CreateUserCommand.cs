﻿using MediatR;
using FluentResults;

namespace CheckflixApp.Application.Identity.Users.Commands.CreateUser;
public class CreateUserCommand : IRequest<Result<string>>
{
    public string Email { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
    public string? PhoneNumber { get; set; }
}