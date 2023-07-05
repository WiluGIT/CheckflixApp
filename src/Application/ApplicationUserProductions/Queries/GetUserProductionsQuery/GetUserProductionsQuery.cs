using CheckflixApp.Application.ApplicationUserProductions.Common;
using CheckflixApp.Application.Common.Security;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.Domain.Entities;
using MediatR;

namespace CheckflixApp.Application.ApplicationUserProductions.Queries.GetUserProductionsQuery;

[Authorize]
public record GetUserProductionsQuery(string UserId) : IRequest<Result<UserCollectionsDto>>;