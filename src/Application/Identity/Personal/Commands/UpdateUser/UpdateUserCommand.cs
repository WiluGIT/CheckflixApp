using CheckflixApp.Application.Common.FileStorage;
using CheckflixApp.Application.Common.Security;
using MediatR;

namespace CheckflixApp.Application.Identity.Personal.Commands.UpdateUser;

[Authorize]
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