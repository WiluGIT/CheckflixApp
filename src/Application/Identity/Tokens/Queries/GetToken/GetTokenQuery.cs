using CheckflixApp.Application.Identity.Dtos;
using MediatR;

namespace CheckflixApp.Application.Identity.Tokens.Queries.GetToken;
public record GetTokenQuery(string Email, string Password) : IRequest<TokenDto>;