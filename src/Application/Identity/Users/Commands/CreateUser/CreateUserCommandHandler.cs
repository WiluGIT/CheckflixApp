using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Application.TodoItems.Commands.CreateTodoItem;
using CheckflixApp.Domain.Entities;
using CheckflixApp.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CheckflixApp.Application.Identity.Users.Commands.CreateUser;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
{
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CreateUserCommandHandler(IUserService userService, IHttpContextAccessor httpContextAccessor)
    {
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.CreateAsync(request, GetOriginFromRequest());
    }

    private string GetOriginFromRequest() => $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}{_httpContextAccessor.HttpContext.Request.PathBase.Value}";
}

