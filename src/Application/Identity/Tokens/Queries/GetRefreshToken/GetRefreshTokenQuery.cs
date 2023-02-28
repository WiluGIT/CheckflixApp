using CheckflixApp.Application.Identity.Common;
using MediatR;

namespace CheckflixApp.Application.Identity.Tokens.Queries.GetRefreshToken;
public record GetRefreshTokenQuery(string Token, string RefreshToken) : IRequest<TokenDto>;