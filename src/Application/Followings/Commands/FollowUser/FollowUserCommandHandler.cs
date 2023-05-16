using CheckflixApp.Application.Common.Exceptions;
using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Domain.Common.Primitives;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.Followings.Commands.FollowUser;

public class FollowUserCommandHandler : IRequestHandler<FollowUserCommand, Result<string>>
{
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IApplicationDbContext _context;
    private readonly IStringLocalizer<FollowUserCommandHandler> _localizer;
    private readonly IFollowedPeopleRepository _followedPeopleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public FollowUserCommandHandler(IUserService userService, ICurrentUserService currentUserService, IApplicationDbContext applicationDbContext, IStringLocalizer<FollowUserCommandHandler> localizer, IFollowedPeopleRepository followedPeopleRepository, IUnitOfWork unitOfWork)
    {
        _userService = userService;
        _currentUserService = currentUserService;
        _context = applicationDbContext;
        _localizer = localizer;
        _followedPeopleRepository = followedPeopleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(FollowUserCommand command, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId ?? string.Empty;

        var target = await _userService.GetAsync(command.UserId, cancellationToken);

        if (target == null || target.Id == null)
        {
            return Error.NotFound(description: _localizer["User Not Found."]);
        }

        if (userId == target.Id)
        {
            return Error.Validation(description: _localizer["You cannot follow yourself"]);
        }

        var followedPeople = await _followedPeopleRepository.GetFollowing(userId, target.Id);
        if (followedPeople != null)
        {
            return Error.Validation(description: _localizer["U are already following this user"]);
        }

        followedPeople = FollowedPeople.Create(userId, target.Id);

        _followedPeopleRepository.Insert(followedPeople);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _localizer["Follow has been added successfully"].Value;
    }
}
