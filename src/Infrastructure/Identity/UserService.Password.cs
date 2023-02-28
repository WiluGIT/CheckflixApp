using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Internal;
using CheckflixApp.Application.Common.Exceptions;
using CheckflixApp.Application.Identity.Users.Commands.ChangePassword;
using CheckflixApp.Application.Identity.Users.Commands.ForgotPassword;
using CheckflixApp.Application.Identity.Users.Commands.ResetPassword;
using Microsoft.AspNetCore.WebUtilities;

namespace CheckflixApp.Infrastructure.Identity;
internal partial class UserService
{
    public async Task<string> ForgotPasswordAsync(ForgotPasswordCommand command, string origin)
    {
        var user = await _userManager.FindByEmailAsync(command.Email.Normalize());
        if (user is null || !await _userManager.IsEmailConfirmedAsync(user))
        {
            // Don't reveal that the user does not exist or is not confirmed
            throw new InternalServerException(_localizer["An Error has occurred!"]);
        }

        // For more information on how to enable account confirmation and password reset please
        // visit https://go.microsoft.com/fwlink/?LinkID=532713
        //string code = await _userManager.GeneratePasswordResetTokenAsync(user);
        //const string route = "account/reset-password";
        //var endpointUri = new Uri(string.Concat($"{origin}/", route));
        //string passwordResetUrl = QueryHelpers.AddQueryString(endpointUri.ToString(), "Token", code);
        //var mailRequest = new MailRequest(
        //    new List<string> { command.Email },
        //    _localizer["Reset Password"],
        //    _localizer[$"Your Password Reset Token is '{code}'. You can reset your password using the {endpointUri} Endpoint."]);
        //_jobService.Enqueue(() => _mailService.SendAsync(mailRequest));

        return _localizer["Password Reset Mail has been sent to your authorized Email."];
    }

    public async Task<string> ResetPasswordAsync(ResetPasswordCommand command)
    {
        var user = await _userManager.FindByEmailAsync(command.Email?.Normalize());

        // Don't reveal that the user does not exist
        _ = user ?? throw new InternalServerException(_localizer["An Error has occurred!"]);

        var result = await _userManager.ResetPasswordAsync(user, command.Token, command.Password);

        return result.Succeeded
            ? _localizer["Password Reset Successful!"]
            : throw new InternalServerException(_localizer["An Error has occurred!"]);
    }

    public async Task ChangePasswordAsync(ChangePasswordCommand command, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        _ = user ?? throw new NotFoundException(_localizer["User Not Found."]);

        var result = await _userManager.ChangePasswordAsync(user, command.Password, command.NewPassword);

        if (!result.Succeeded)
        {
            throw new InternalServerException(_localizer["Change password failed"]);
        }
    }
}
