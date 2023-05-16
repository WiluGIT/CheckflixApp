using CheckflixApp.Application.Common.Security;
using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Identity.Roles.Commands.CreateOrUpdateRole;

[Authorize(Roles = "Administrator")]
public class CreateOrUpdateRoleCommand : IRequest<Result<string>>
{
    public string? Id { get; set; }
    public string Name { get; set; } = default!;
}
