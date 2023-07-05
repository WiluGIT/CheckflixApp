using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Followings.Common;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Followings.Queries.GetFollowersQuery;

public class GetFollowersQueryHandler : IRequestHandler<GetFollowersQuery, Result<List<UserWithFollowingDto>>>
{
    private readonly IFollowedPeopleRepository _followedPeopleRepository;

    public GetFollowersQueryHandler(IFollowedPeopleRepository followedPeopleRepository)
    {
        _followedPeopleRepository = followedPeopleRepository;
    }

    public async Task<Result<List<UserWithFollowingDto>>> Handle(GetFollowersQuery query, CancellationToken cancellationToken)
        => await _followedPeopleRepository.GetUserFollowersByIdAsync(query.UserId);
}
