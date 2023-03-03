using CheckflixApp.Application.Common.Security;
using CheckflixApp.Application.Identity.Common;
using MediatR;

namespace CheckflixApp.Application.Identity.Personal.Queries.GetProfile;

[Authorize]
public record GetProfileQuery() : IRequest<UserDetailsDto>;