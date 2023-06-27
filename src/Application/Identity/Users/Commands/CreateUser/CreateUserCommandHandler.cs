using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Domain.Common.Errors;
using CheckflixApp.Domain.Common.Primitives.Result;
using CheckflixApp.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.Identity.Users.Commands.CreateUser;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<(string, string)>>
{
    private readonly IIdentityService _identityService;
    private readonly IStringLocalizer<CreateUserCommandHandler> _localizer;

    public CreateUserCommandHandler(IIdentityService identityService, IStringLocalizer<CreateUserCommandHandler> localizer)
    {
        _identityService = identityService;
        _localizer = localizer;
    }

    public async Task<Result<(string, string)>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        Result<UserName> userNameResult = UserName.Create(command.UserName);
        Result<Email> emailResult = Email.Create(command.Email);
        Result<Password> passwordResult = Password.Create(command.Password);

        var errorList = Result.GetErrorsFromFailureResults(userNameResult, emailResult, passwordResult);

        if (errorList.Any())
        {
            return errorList;
        }

        if (!await _identityService.IsEmailUniqueAsync(emailResult.Value))
        {
            return DomainErrors.User.DuplicateEmail;
        }

        if (!await _identityService.IsUserNameUniqueAsync(userNameResult.Value))
        {
            return DomainErrors.User.DuplicateUserName;
        }

        var (result, user) = await _identityService.CreateUserAsync(emailResult.Value, userNameResult.Value, passwordResult.Value);
        if (result.IsFailure)
        {
            return DomainErrors.User.PasswordValidationError;
        }

        return (string.Format(_localizer["User {0} Registered."].Value, emailResult.Value), user.Id);
    }
}

