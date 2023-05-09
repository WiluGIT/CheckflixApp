using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CheckflixApp.Application.Identity.Users.Queries.GetList;

public class GetListQueryHandler : IRequestHandler<GetListQuery, Result<List<UserDetailsDto>>>
{
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public GetListQueryHandler(IUserService userService, IHttpContextAccessor httpContextAccessor)
    {
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<List<UserDetailsDto>>> Handle(GetListQuery query, CancellationToken cancellationToken)
    {
        return await _userService.GetListAsync(cancellationToken);
    }
}
