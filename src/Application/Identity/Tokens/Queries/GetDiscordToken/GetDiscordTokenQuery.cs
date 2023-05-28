using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Identity.Tokens.Queries.GetDiscordToken;

public record GetDiscordTokenQuery(string Code) : IRequest<Result<TokenDto>>;