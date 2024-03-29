﻿using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Domain.Common.Primitives;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.Identity.Personal.Queries.GetProfile;

public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, Result<UserDetailsDto>>
{
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IStringLocalizer<GetProfileQueryHandler> _localizer;

    public GetProfileQueryHandler(IUserService userService, ICurrentUserService currentUserService, IStringLocalizer<GetProfileQueryHandler> localizer)
    {
        _userService = userService;
        _currentUserService = currentUserService;
        _localizer = localizer;
    }

    public async Task<Result<UserDetailsDto>> Handle(GetProfileQuery query, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId ?? string.Empty;

        var userProfile = await _userService.GetAsync(query.UserId, cancellationToken);

        if (userProfile == null)
        {
            return Error.NotFound(description: _localizer["User Not Found."]);
        }

        return userProfile;
    }
}
