using CheckflixApp.Domain.Common.Errors;
using CheckflixApp.Domain.Common.Primitives;
using CheckflixApp.Domain.Common.Primitives.Result;

namespace CheckflixApp.Domain.ValueObjects;

/// <summary>
/// Represents the first name value object.
/// </summary>
public sealed class UserName : ValueObject
{
    /// <summary>
    /// The first name maximum length.
    /// </summary>
    public const int MaxLength = 100;

    /// <summary>
    /// Initializes a new instance of the <see cref="FirstName"/> class.
    /// </summary>
    /// <param name="value">The first name value.</param>
    private UserName(string value) => Value = value;

    /// <summary>
    /// Gets the first name value.
    /// </summary>
    public string Value { get; }

    public static implicit operator string(UserName firstName) => firstName.Value;

    /// <summary>
    /// Creates a new <see cref="FirstName"/> instance based on the specified value.
    /// </summary>
    /// <param name="firstName">The first name value.</param>
    /// <returns>The result of the first name creation process containing the first name or an error.</returns>
    public static Result<UserName> Create(string firstName) =>
        new List<Error>()
            .Ensure(firstName, f => !string.IsNullOrWhiteSpace(f), DomainErrors.UserName.NullOrEmpty)
            .Ensure(firstName, f => f.Length <= MaxLength, DomainErrors.UserName.LongerThanAllowed) 
        is var validationErrors && validationErrors.Any() ?  
        validationErrors :
        Result.From(new UserName(firstName));

    /// <inheritdoc />
    public override string ToString() => Value;

    /// <inheritdoc />
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}