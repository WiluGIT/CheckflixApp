using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CheckflixApp.Application.Identity.Users.Commands.ResetPassword;
public class ResetPasswordCommand : IRequest<string>
{
    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Token { get; set; }
}