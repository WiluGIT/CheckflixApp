using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckflixApp.Application.Common.Security;
using MediatR;

namespace CheckflixApp.Application.Identity.Users.Commands.ToggleUserStatus;

[Authorize(Roles = "Administrator")]
public record ToggleUserStatusCommand(string? Id, bool ActivateUser) : IRequest<Unit>;