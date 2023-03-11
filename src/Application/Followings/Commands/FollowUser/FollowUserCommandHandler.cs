using CheckflixApp.Application.Common.Exceptions;
using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.Followings.Commands.FollowUser;

public class FollowUserCommandHandler : IRequestHandler<FollowUserCommand, string>
{
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IApplicationDbContext _context;
    private readonly IStringLocalizer<FollowUserCommandHandler> _localizer;

    public FollowUserCommandHandler(IUserService userService, ICurrentUserService currentUserService, IApplicationDbContext applicationDbContext, IStringLocalizer<FollowUserCommandHandler> localizer)
    {
        _userService = userService;
        _currentUserService = currentUserService;
        _context = applicationDbContext;
        _localizer = localizer;
    }

    public async Task<string> Handle(FollowUserCommand command, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId ?? string.Empty;

        var target = await _userService.GetAsync(command.UserId, cancellationToken);

        if (target == null || target.Id == null)
        {
            throw new NotFoundException(_localizer["User Not Found."]);
        }

        if (userId == target.Id)
        {
            throw new InternalServerException(_localizer["You cannot follow yourself"]);
        }

        var followedPeople = await _context.FollowedPeople
            .FirstOrDefaultAsync(x => x.ObserverId == userId && x.TargetId == target.Id, cancellationToken);

        if (followedPeople != null)
        {
            throw new InternalServerException(_localizer["U are already following this user"]);
        }

        followedPeople = FollowedPeople.Create(userId, target.Id);

        await _context.FollowedPeople.AddAsync(followedPeople, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return _localizer["Follow has been added successfully"];
    }
}
