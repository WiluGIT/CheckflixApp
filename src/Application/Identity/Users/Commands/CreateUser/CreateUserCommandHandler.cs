using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Domain.Common.Errors;
using CheckflixApp.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Localization;
using FluentResults;

namespace CheckflixApp.Application.Identity.Users.Commands.CreateUser;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<string>>
{
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStringLocalizer<CreateUserCommandHandler> _localizer;

    public CreateUserCommandHandler(IIdentityService identityService, IUnitOfWork unitOfWork, IStringLocalizer<CreateUserCommandHandler> localizer)
    {
        _identityService = identityService;
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<Result<string>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        Result<UserName> userNameResult = UserName.Create(command.UserName);
        //Result<Email> emailResult = Email.Create(command.Email);
        //Result<Password> passwordResult = Password.Create(command.Password);

        //Result firstFailureOrSuccess = Result.FirstFailureOrSuccess(userNameResult, emailResult, passwordResult);

        //if (firstFailureOrSuccess.IsFailure)
        //{
        //    return Result.Failure<string>(firstFailureOrSuccess.Error);
        //}
        //string xd = emailResult.Value.Value;
        //var ds = emailResult.Value;
        //if (!await _identityService.IsEmailUniqueAsync(emailResult.Value))
        //{
        //    return Result.Failure<string>(DomainErrors.User.DuplicateEmail);
        //}

        //var (result, id) = await _identityService.CreateUserAsync(userNameResult.Value, passwordResult.Value);
        //if (result.IsFailure)
        //{
        //    return Result.Failure<string>(DomainErrors.User.PasswordValidationError);
        //}

        //await _unitOfWork.SaveChangesAsync(cancellationToken);

        //Add domain 
        //return Result.Success(string.Format(_localizer["User {@UserName} Registered."], userNameResult.Value));

        //var xd = userNameResult.Value.Value;

        if (userNameResult.IsFailed)
        {
            return Result.Fail<string>(userNameResult.Errors);
        }
        return Result.Ok(userNameResult.Value.Value);
    }
}

