using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Application.Identity.Users.Commands.CreateUser;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CheckflixApp.Application.Identity.Users.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result<string>>
{
    private readonly IUserService _userService;

    public ConfirmEmailCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result<string>> Handle(ConfirmEmailCommand command, CancellationToken cancellationToken)
    {
        return await _userService.ConfirmEmailAsync(command.UserId, command.Code, cancellationToken);
    }
}

