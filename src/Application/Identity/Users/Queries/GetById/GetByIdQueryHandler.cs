using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using MediatR;

namespace CheckflixApp.Application.Identity.Users.Queries.GetById;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, UserDetailsDto>
{
    private readonly IUserService _userService;
    public GetByIdQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UserDetailsDto> Handle(GetByIdQuery query, CancellationToken cancellationToken)
    {
        return await _userService.GetAsync(query.Id, cancellationToken);
    }
}