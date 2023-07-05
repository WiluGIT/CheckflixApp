using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Followings.Common;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Followings.Queries.SearchUsers;

public class SearchUsersQueryHandler : IRequestHandler<SearchUsersQuery, Result<List<UserWithFollowingDto>>>
{
    private readonly IFollowedPeopleRepository _followedPeopleRepository;
    private readonly ICurrentUserService _currentUserService;

    public SearchUsersQueryHandler(IFollowedPeopleRepository followedPeopleRepository, ICurrentUserService currentUserService)
    {
        _followedPeopleRepository = followedPeopleRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Result<List<UserWithFollowingDto>>> Handle(SearchUsersQuery query, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId ?? string.Empty;
        return await _followedPeopleRepository.SearchUsersWithFollowingAsync(query.SearchTerm, userId, query.Count);
    }
}
