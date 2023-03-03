using CheckflixApp.Application.Common.Security;
using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.Identity.Roles.Commands.CreateOrUpdateRole;

[Authorize(Roles = "Administrator")]
public class CreateOrUpdateRoleCommand : IRequest<string>
{
    public string? Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

public class CreateOrUpdateRoleCommandValidator : AbstractValidator<CreateOrUpdateRoleCommand>
{
    public CreateOrUpdateRoleCommandValidator(IRoleService roleService, IStringLocalizer<CreateOrUpdateRoleCommandValidator> T) =>
        RuleFor(r => r.Name)
            .NotEmpty()
            .MustAsync(async (role, name, _) => !await roleService.ExistsAsync(name, role.Id))
                .WithMessage(T["Similar Role already exists."]);
}