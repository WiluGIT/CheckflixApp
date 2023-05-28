using CheckflixApp.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CheckflixApp.Application.Productions.Commands.UpdateProductionCommand;

public class UpdateProductionCommandValidator : AbstractValidator<UpdateProductionCommand>
{
    public UpdateProductionCommandValidator(IStringLocalizer<UpdateProductionCommandValidator> T, IGenreRepository genreRepository)
    {
        RuleFor(p => p.TmdbId)
            .NotEmpty();

        RuleFor(p => p.ImdbId)
            .NotEmpty();

        RuleFor(p => p.Title)
            .NotEmpty();

        RuleFor(p => p.Director)
            .NotEmpty();

        RuleFor(p => p.Keywords)
            .NotEmpty();

        RuleFor(p => p.Overview)
            .NotEmpty();

        RuleFor(p => p.GenreIds)
            .NotEmpty()
            .Must(x => x.Any())
            .MustAsync(async (command, genreIds, _) => await genreRepository.ValidateIfGenresExists(genreIds))
            .WithMessage(T["One or more specified genreIds does not exist"]);
    }
}