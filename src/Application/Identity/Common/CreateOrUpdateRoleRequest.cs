﻿using CheckflixApp.Application.Identity.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.Identity.Common;
public class CreateOrUpdateRoleRequest
{
    public string? Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

public class CreateOrUpdateRoleRequestValidator : AbstractValidator<CreateOrUpdateRoleRequest>
{
    public CreateOrUpdateRoleRequestValidator(IRoleService roleService, IStringLocalizer<CreateOrUpdateRoleRequestValidator> T) =>
        RuleFor(r => r.Name)
            .NotEmpty()
            .MustAsync(async (role, name, _) => !await roleService.ExistsAsync(name, role.Id))
                .WithMessage(T["Similar Role already exists."]);
}