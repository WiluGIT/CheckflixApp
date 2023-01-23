using FluentValidation;

namespace CheckflixApp.Application.Identity.Tokens.Queries.GetToken;
public class GetTokenQueryValidator : AbstractValidator<GetTokenQuery>
{
    public GetTokenQueryValidator()
    {
        RuleFor(p => p.Email).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress()
                .WithMessage("Invalid Email Address.");

        RuleFor(p => p.Password).Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}
