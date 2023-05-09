using CheckflixApp.Application.Common.Exceptions;
using CheckflixApp.Application.Identity.Personal.Commands.ChangePassword;
using CheckflixApp.Application.Identity.Users.Commands.ForgotPassword;
using CheckflixApp.Application.Identity.Users.Commands.ResetPassword;
using CheckflixApp.Application.Mailing;
using CheckflixApp.Domain.Common.Primitives;
using CheckflixApp.Domain.Common.Primitives.Result;
using Microsoft.AspNetCore.WebUtilities;

namespace CheckflixApp.Infrastructure.Identity;
internal partial class UserService
{
    public async Task<Result<string>> ForgotPasswordAsync(ForgotPasswordCommand command, string origin)
    {
        var user = await _userManager.FindByEmailAsync(command.Email.Normalize());
        if (user is null || !await _userManager.IsEmailConfirmedAsync(user))
        {
            return Error.Validation(description: _localizer["An Error has occurred!"]);
        }

        // For more information on how to enable account confirmation and password reset please
        // visit https://go.microsoft.com/fwlink/?LinkID=532713
        // To change code expiry date use DataProtectionTokenProviderOptions in startup
        string code = await _userManager.GeneratePasswordResetTokenAsync(user);
        const string route = "account/reset-password";
        var endpointUri = new Uri(string.Concat($"{origin}/", route));
        string passwordResetUrl = QueryHelpers.AddQueryString(endpointUri.ToString(), "Token", code);

        var mailRequest = new MailRequest(
            new List<string> { command.Email },
            _localizer["Reset Password"],
            _localizer[$"Your Password Reset Token is '{code}'. You can reset your password using the {endpointUri} Endpoint."]);

        _jobService.Enqueue(() => _mailService.SendAsync(mailRequest, CancellationToken.None));

        return _localizer["Password Reset Mail has been sent to your authorized Email."].Value;
    }

    public async Task<Result<string>> ResetPasswordAsync(ResetPasswordCommand command)
    {
        var user = await _userManager.FindByEmailAsync(command.Email?.Normalize());

        // Don't reveal that the user does not exist
        if (user == null)
        {
            Error.Validation(description: _localizer["An Error has occurred!"]);
        }

        var result = await _userManager.ResetPasswordAsync(user, command.Token, command.Password);

        return result.Succeeded
            ? _localizer["Password Reset Successful!"].Value
            : Error.Validation(description: _localizer["An Error has occurred!"]);
    }

    public async Task<Result<string>> ChangePasswordAsync(ChangePasswordCommand command, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null) 
        {
            return Error.NotFound(description: _localizer["User Not Found."]);
        }

        var result = await _userManager.ChangePasswordAsync(user, command.Password, command.NewPassword);

        if (!result.Succeeded)
        {
            return Error.Failure(description: _localizer["Change password failed"]);
        }

        return _localizer["Change password succeeded"].Value;
    }
}
