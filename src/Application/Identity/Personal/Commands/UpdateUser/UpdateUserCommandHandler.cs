using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Identity.Personal.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<string>>
{
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;

    public UpdateUserCommandHandler(IUserService userService, ICurrentUserService currentUserService)
    {
        _userService = userService;
        _currentUserService = currentUserService;
    }

    public async Task<Result<string>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId ?? string.Empty;

        var updateResult = await _userService.UpdateAsync(command, userId);

        if (updateResult.IsFailure)
        {
            return updateResult.Errors;
        }

        return updateResult;
    }
}
