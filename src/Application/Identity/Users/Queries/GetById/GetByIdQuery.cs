using CheckflixApp.Application.Identity.Common;
using MediatR;

namespace CheckflixApp.Application.Identity.Users.Queries.GetById;

public record GetByIdQuery(string Id) : IRequest<UserDetailsDto>;