using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Identity.Users.Commands.ConfirmPhoneNumber;

public class ConfirmPhoneNumerCommand : IRequest<Result<string>>
{
    public string UserId { get; set; } = default!;
    public string Code { get; set; } = default!;
}