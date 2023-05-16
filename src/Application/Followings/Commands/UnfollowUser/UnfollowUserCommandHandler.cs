using CheckflixApp.Application.Common.Exceptions;
using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Followings.Commands.FollowUser;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Domain.Common.Primitives;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.Followings.Commands.UnfollowUser;

public class UnfollowUserCommandHandler : IRequestHandler<UnfollowUserCommand, Result<string>>
{
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IStringLocalizer<FollowUserCommandHandler> _localizer;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFollowedPeopleRepository _followedPeopleRepository;

    public UnfollowUserCommandHandler(IUserService userService, ICurrentUserService currentUserService, IStringLocalizer<FollowUserCommandHandler> localizer, IUnitOfWork unitOfWork, IFollowedPeopleRepository followedPeopleRepository)
    {
        _userService = userService;
        _currentUserService = currentUserService;
        _localizer = localizer;
        _unitOfWork = unitOfWork;
        _followedPeopleRepository = followedPeopleRepository;
    }

    public async Task<Result<string>> Handle(UnfollowUserCommand command, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId ?? string.Empty;

        var target = await _userService.GetAsync(command.UserId, cancellationToken);

        if (target == null || target.Id == null)
        {
            return Error.NotFound(description: _localizer["User Not Found."]);
        }

        if (userId == target.Id)
        {
            return Error.Validation(description: _localizer["You cannot unfollow yourself"]);
        }

        var followedPeople = await _followedPeopleRepository.GetFollowing(userId, target.Id);
        if (followedPeople == null)
        {
            return Error.Validation(description: _localizer["U are not following this user"]);
        }

        _followedPeopleRepository.Remove(followedPeople);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _localizer["User has been unfollowed successfully"].Value;
    }
}
