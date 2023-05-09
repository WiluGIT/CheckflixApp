using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Identity.Users.Commands.ConfirmEmail;

public class ConfirmEmailCommand : IRequest<Result<string>>
{
    public string UserId { get; set; } = default!;
    public string Code { get; set; } = default!;
}