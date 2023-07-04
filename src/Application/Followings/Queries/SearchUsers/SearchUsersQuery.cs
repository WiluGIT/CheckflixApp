using CheckflixApp.Application.Common.Security;
using CheckflixApp.Application.Followings.Common;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Followings.Queries.SearchUsers;

[Authorize]
public record SearchUsersQuery(string SearchTerm, int Count) : IRequest<Result<List<UserWithFollowingDto>>>;