using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Identity.Tokens.Queries.GetToken;
public record GetTokenQuery(string Email, string Password) : IRequest<Result<UserInfoDto>>;