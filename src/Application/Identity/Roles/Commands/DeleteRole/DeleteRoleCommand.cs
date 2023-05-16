using CheckflixApp.Application.Common.Security;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Identity.Roles.Commands.DeleteRole;

[Authorize(Roles = "Administrator")]
public record DeleteRoleCommand(string Id) : IRequest<Result<string>>;