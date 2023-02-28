using System.Security.Claims;
using CheckflixApp.Application.Common.Exceptions;
using CheckflixApp.Application.Identity.Users.Commands.CreateUser;
using CheckflixApp.Application.Identity.Users.Commands.UpdateUser;
using CheckflixApp.Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;

namespace CheckflixApp.Infrastructure.Identity;
internal partial class UserService
{
    public async Task<string> GetOrCreateFromPrincipalAsync(ClaimsPrincipal principal)
    {
        string? objectId = principal.GetObjectId();
        if (string.IsNullOrWhiteSpace(objectId))
        {
            throw new InternalServerException(_localizer["Invalid objectId"]);
        }

        var user = await _userManager.Users.Where(u => u.ObjectId == objectId).FirstOrDefaultAsync()
            ?? await CreateOrUpdateFromPrincipalAsync(principal);

        if (principal.FindFirstValue(ClaimTypes.Role) is string role &&
            await _roleManager.RoleExistsAsync(role) &&
            !await _userManager.IsInRoleAsync(user, role))
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        return user.Id;
    }

    private async Task<ApplicationUser> CreateOrUpdateFromPrincipalAsync(ClaimsPrincipal principal)
    {
        string? email = principal.FindFirstValue(ClaimTypes.Upn);
        string? username = principal.GetDisplayName();
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username))
        {
            throw new InternalServerException(string.Format(_localizer["Username or Email not valid."]));
        }

        var user = await _userManager.FindByNameAsync(username);
        if (user is not null && !string.IsNullOrWhiteSpace(user.ObjectId))
        {
            throw new InternalServerException(string.Format(_localizer["Username {0} is already taken."], username));
        }

        if (user is null)
        {
            user = await _userManager.FindByEmailAsync(email);
            if (user is not null && !string.IsNullOrWhiteSpace(user.ObjectId))
            {
                throw new InternalServerException(string.Format(_localizer["Email {0} is already taken."], email));
            }
        }

        IdentityResult? result;
        if (user is not null)
        {
            user.ObjectId = principal.GetObjectId();
            result = await _userManager.UpdateAsync(user);

            //await _events.PublishAsync(new ApplicationUserUpdatedEvent(user.Id));
        }
        else
        {
            user = new ApplicationUser
            {
                ObjectId = principal.GetObjectId(),
                Email = email,
                NormalizedEmail = email.ToUpperInvariant(),
                UserName = username,
                NormalizedUserName = username.ToUpperInvariant(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            };
            result = await _userManager.CreateAsync(user);

            //await _events.PublishAsync(new ApplicationUserCreatedEvent(user.Id));
        }

        if (!result.Succeeded)
        {
            throw new InternalServerException(_localizer["Validation Errors Occurred."]);
        }

        return user;
    }

    public async Task<string> CreateAsync(CreateUserCommand command, string origin)
    {
        var user = new ApplicationUser
        {
            Email = command.Email,
            UserName = command.UserName,
            PhoneNumber = command.PhoneNumber,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, command.Password);
        if (!result.Succeeded)
        {
            throw new InternalServerException(_localizer["Validation Errors Occurred."]);
        }

        await _userManager.AddToRoleAsync(user, ApplicationRoles.Basic);

        var messages = new List<string> { string.Format(_localizer["User {0} Registered."], user.UserName) };

        //if (_securitySettings.RequireConfirmedAccount && !string.IsNullOrEmpty(user.Email))
        //{
        //    // send verification email
        //    string emailVerificationUri = await GetEmailVerificationUriAsync(user, origin);
        //    RegisterUserEmailModel eMailModel = new RegisterUserEmailModel()
        //    {
        //        Email = user.Email,
        //        UserName = user.UserName,
        //        Url = emailVerificationUri
        //    };
        //    var mailRequest = new MailRequest(
        //        new List<string> { user.Email },
        //        _localizer["Confirm Registration"],
        //        _templateService.GenerateEmailTemplate("email-confirmation", eMailModel));
        //    _jobService.Enqueue(() => _mailService.SendAsync(mailRequest));
        //    messages.Add(_localizer[$"Please check {user.Email} to verify your account!"]);
        //}

        //await _events.PublishAsync(new ApplicationUserCreatedEvent(user.Id));

        return string.Join(Environment.NewLine, messages);
    }

    public async Task UpdateAsync(UpdateUserCommand command, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        _ = user ?? throw new NotFoundException(_localizer["User Not Found."]);

        string currentImage = user.ImageUrl ?? string.Empty;
        if (command.Image != null || command.DeleteCurrentImage)
        {
            //user.ImageUrl = await _fileStorage.UploadAsync<ApplicationUser>(command.Image, FileType.Image);
            if (command.DeleteCurrentImage && !string.IsNullOrEmpty(currentImage))
            {
                string root = Directory.GetCurrentDirectory();
                //_fileStorage.Remove(Path.Combine(root, currentImage));
            }
        }

        user.PhoneNumber = command.PhoneNumber;
        string phoneNumber = await _userManager.GetPhoneNumberAsync(user);
        if (command.PhoneNumber != phoneNumber)
        {
            await _userManager.SetPhoneNumberAsync(user, command.PhoneNumber);
        }

        var result = await _userManager.UpdateAsync(user);

        await _signInManager.RefreshSignInAsync(user);

        //await _events.PublishAsync(new ApplicationUserUpdatedEvent(user.Id));

        if (!result.Succeeded)
        {
            throw new InternalServerException(_localizer["Update profile failed"]);
        }
    }
}