using CheckflixApp.Application.Common.Security;
using System.Data;
using CheckflixApp.Application.Identity.Common;
using MediatR;

namespace CheckflixApp.Application.Identity.Users.Queries.GetList;

[Authorize(Roles = "Administrator")]
public record GetListQuery() : IRequest<List<UserDetailsDto>>;