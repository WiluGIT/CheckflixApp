using CheckflixApp.Application.Identity.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.Identity.Roles.Commands.CreateOrUpdateRole;

public class CreateOrUpdateRoleCommandValidator : AbstractValidator<CreateOrUpdateRoleCommand>
{
    public CreateOrUpdateRoleCommandValidator(IRoleService roleService, IStringLocalizer<CreateOrUpdateRoleCommandValidator> T) =>
        RuleFor(r => r.Name)
            .NotEmpty()
            .MustAsync(async (role, name, _) => !await roleService.ExistsAsync(name, role.Id))
                .WithMessage(T["Similar Role already exists."]);
}