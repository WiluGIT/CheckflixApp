﻿using CheckflixApp.Application.Common.Exceptions;
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
    private readonly IApplicationDbContext _context;
    private readonly IStringLocalizer<FollowUserCommandHandler> _localizer;

    public UnfollowUserCommandHandler(IUserService userService, ICurrentUserService currentUserService, IApplicationDbContext applicationDbContext, IStringLocalizer<FollowUserCommandHandler> localizer)
    {
        _userService = userService;
        _currentUserService = currentUserService;
        _context = applicationDbContext;
        _localizer = localizer;
    }

    public async Task<Result<string>> Handle(UnfollowUserCommand command, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId ?? string.Empty;

        var target = await _userService.GetAsync(command.UserId, cancellationToken);

        if (target == null)
        {
            return Error.NotFound(description: _localizer["User Not Found."]);
        }

        if (userId == target.Id)
        {
            return Error.Validation(description: _localizer["You cannot unfollow yourself"]);
        }

        var followedPeople = await _context.FollowedPeople
            .FirstOrDefaultAsync(x => x.ObserverId == userId && x.TargetId == target.Id, cancellationToken);

        if (followedPeople == null)
        {
            return Error.Validation(description: _localizer["U are not following this user"]);
        }

        // TODO rep and unit
        _context.FollowedPeople.Remove(followedPeople);
        await _context.SaveChangesAsync(cancellationToken);

        return _localizer["User has been unfollowed successfully"].Value;
    }
}
