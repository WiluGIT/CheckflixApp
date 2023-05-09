using CheckflixApp.Application.Common.Security;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CheckflixApp.Application.Identity.Personal.Commands.UpdateUser;

[Authorize]
public class UpdateUserCommand : IRequest<Result<string>>
{
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public IFormFile? Image { get; set; }
    public bool DeleteCurrentImage { get; set; } = false;
}