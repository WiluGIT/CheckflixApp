using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Identity.Users.Commands.ForgotPassword;

public class ForgotPasswordCommand : IRequest<Result<string>>
{
    public string Email { get; set; } = default!;
}
