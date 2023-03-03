using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using MediatR;

namespace CheckflixApp.Application.Identity.Personal.Queries.GetProfile;

public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, UserDetailsDto>
{
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;

    public GetProfileQueryHandler(IUserService userService, ICurrentUserService currentUserService)
    {
        _userService = userService;
        _currentUserService = currentUserService;
    }

    public async Task<UserDetailsDto> Handle(GetProfileQuery query, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId ?? string.Empty;

        return await _userService.GetAsync(userId, cancellationToken);
    }
}
