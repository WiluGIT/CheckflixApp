using CheckflixApp.Application.Identity.Dtos;
using CheckflixApp.Application.Identity.Tokens.Interfaces;
using MediatR;

namespace CheckflixApp.Application.Identity.Tokens.Queries.GetToken;
public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, TokenDto>
{
    private readonly ITokenService _tokenService;
    public GetTokenQueryHandler(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task<TokenDto> Handle(GetTokenQuery query, CancellationToken cancellationToken)
    {
        var token = await _tokenService.GetTokenAsync(query, "test", cancellationToken);

        return token;
    }
}