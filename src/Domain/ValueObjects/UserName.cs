using CheckflixApp.Domain.Common.Errors;
using CheckflixApp.Domain.Common.Primitives;
using FluentResults;

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
    //public static Result<UserName> Create(string firstName) =>
    //    Result.Create(firstName, DomainErrors.FirstName.NullOrEmpty)
    //        .Ensure(f => !string.IsNullOrWhiteSpace(f), DomainErrors.FirstName.NullOrEmpty)
    //        .Ensure(f => f.Length <= MaxLength, DomainErrors.FirstName.LongerThanAllowed)
    //        .Map(f => new UserName(f));

    public static Result<UserName> Create(string firstName)
    {
        var valResult = Result.Merge(
                Result.FailIf(!string.IsNullOrWhiteSpace(firstName), DomainErrors.FirstName.NullOrEmpty.Message),
                Result.FailIf(firstName.Length <= MaxLength, DomainErrors.FirstName.LongerThanAllowed.Message)
            );

        var z = Result.Ok(new UserName(firstName));

        if (valResult.IsFailed)
        {
            return valResult;
        }

       

        return z;
    }

    /// <inheritdoc />
    public override string ToString() => Value;

    /// <inheritdoc />
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}