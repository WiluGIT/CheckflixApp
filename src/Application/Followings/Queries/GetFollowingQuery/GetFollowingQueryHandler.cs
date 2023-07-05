using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Followings.Common;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Followings.Queries.GetFollowingQuery;

public class GetFollowingQueryHandler : IRequestHandler<GetFollowingQuery, Result<List<UserWithFollowingDto>>>
{
    private readonly IFollowedPeopleRepository _followedPeopleRepository;

    public GetFollowingQueryHandler(IFollowedPeopleRepository followedPeopleRepository)
    {
        _followedPeopleRepository = followedPeopleRepository;
    }

    public async Task<Result<List<UserWithFollowingDto>>> Handle(GetFollowingQuery query, CancellationToken cancellationToken)
        => await _followedPeopleRepository.GetUserFollowingsByIdAsync(query.UserId);
}
