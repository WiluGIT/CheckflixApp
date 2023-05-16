using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Domain.Common.Primitives;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.Identity.Users.Queries.GetById;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Result<UserDetailsDto>>
{
    private readonly IUserService _userService;
    private readonly IStringLocalizer<GetByIdQueryHandler> _localizer;

    public GetByIdQueryHandler(IUserService userService, IStringLocalizer<GetByIdQueryHandler> localizer)
    {
        _userService = userService;
        _localizer = localizer;
    }

    public async Task<Result<UserDetailsDto>> Handle(GetByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await _userService.GetAsync(query.Id, cancellationToken);

        if (user == null)
        {
            return Error.NotFound(description: _localizer["User Not Found."]);
        }

        return user;
    }
}