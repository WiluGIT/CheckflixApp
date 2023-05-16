using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Productions.Commands.CreateProductionCommand;
public class CreateProductionCommand : IRequest<Result<string>>
{
    public string TmdbId { get; set; } = string.Empty;
    public string ImdbId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
}
