using CheckflixApp.Application.Common.Security;
using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Identity.Roles.Queries.GetRoleById;

[Authorize(Roles = "Administrator")]
public record GetRoleByIdQuery(string Id) : IRequest<Result<RoleDto>>;