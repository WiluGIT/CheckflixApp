using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CheckflixApp.Application.Identity.Users.Commands.ToggleUserStatus;
public record ToggleUserStatusCommand(string? Id, bool ActivateUser) : IRequest<Unit>;