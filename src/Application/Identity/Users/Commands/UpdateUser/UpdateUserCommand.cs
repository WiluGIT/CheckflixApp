using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckflixApp.Application.Common.FileStorage;
using MediatR;

namespace CheckflixApp.Application.Identity.Users.Commands.UpdateUser;
public class UpdateUserCommand : IRequest<string>
{
    public string Id { get; set; } = default!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public FileUploadCommand? Image { get; set; }
    public bool DeleteCurrentImage { get; set; } = false;
}