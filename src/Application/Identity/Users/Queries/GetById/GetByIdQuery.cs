using CheckflixApp.Application.Common.Security;
using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Identity.Users.Queries.GetById;

[Authorize(Roles = "Administrator")]
public record GetByIdQuery(string Id) : IRequest<Result<UserDetailsDto>>;