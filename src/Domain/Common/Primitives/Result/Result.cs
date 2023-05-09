namespace CheckflixApp.Domain.Common.Primitives.Result;

/// <summary>
/// A discriminated union of Result Object.
/// </summary>
public class Result : IResult
{
    private readonly List<Error>? _errors = null;

    private static readonly Error NoFirstError = Error.Unexpected(
        code: "Result.NoFirstError",
        description: "First error cannot be retrieved from a successful Result.");

    private static readonly Error NoErrors = Error.Unexpected(
        code: "Result.NoErrors",
        description: "Error list cannot be retrieved from a successful Result.");

    /// <summary>
    /// Gets a value indicating whether the state is error.
    /// </summary>
    public bool IsFailure { get; }

    /// <summary>
    /// Gets the list of errors. If the state is not error, the list will contain a single error representing the state.
    /// </summary>
    public List<Error> Errors => IsFailure ? _errors! : new List<Error> { NoErrors };

    /// <summary>
    /// Gets the list of errors. If the state is not error, the list will be empty.
    /// </summary>
    public List<Error> ErrorsOrEmptyList => IsFailure ? _errors! : new();

    /// <summary>
    /// Gets the first error.
    /// </summary>
    public Error FirstError
    {
        get
        {
            if (!IsFailure)
            {
                return NoFirstError;
            }

            return _errors![0];
        }
    }

    protected Result(Error error)
    {
        _errors = new List<Error> { error };
        IsFailure = true;
    }

    protected Result(List<Error> errors)
    {
        _errors = errors;
        IsFailure = true;
    }
    protected Result()
    {
        IsFailure = false;
    }

    public static Result<TValue> From<TValue>(TValue value)
    {
        return value;
    }

    public static Result From(List<Error>? errors = null)
    {
        return errors != null ? new Result(errors) : new Result();
    }

    /// <summary>
    /// Creates an <see cref="Result{TValue}"/> from a error list.
    /// </summary>
    public static implicit operator Result(List<Error> errors)
    {
        return new Result(errors);
    }

    /// <summary>
    /// Creates an <see cref="Result{TValue}"/> from a error.
    /// </summary>
    public static implicit operator Result(Error errors)
    {
        return new Result(errors);
    }

    /// <summary>
    /// Returns all errors from Results with failure <paramref name="results"/>.
    /// If there is a failure. Error list is returned.
    /// </summary>
    /// <param name="results">The error list.</param>
    /// <returns>
    /// The error list from specified results with failure <paramref name="results"/>.
    /// </returns>
    public static List<Error> GetErrorsFromFailureResults(params Result[] results) =>
        results.Where(x => x.IsFailure).SelectMany(x => x.Errors).ToList();
}

public class Result<TValue> : Result
{
    private readonly TValue? _value = default;

    /// <summary>
    /// Gets the value.
    /// </summary>
    public TValue Value => _value!;

    private Result(TValue value) : base()
    {
        _value = value;
    }

    protected Result(Error error) : base(error) { }

    protected Result(List<Error> errors) : base(errors) { }

    /// <summary>
    /// Creates an <see cref="Result{TValue}"/> from a value.
    /// </summary>
    public static implicit operator Result<TValue>(TValue value)
    {
        return new Result<TValue>(value);
    }

    /// <summary>
    /// Creates an <see cref="Result{TValue}"/> from an error.
    /// </summary>
    public static implicit operator Result<TValue>(Error error)
    {
        return new Result<TValue>(error);
    }

    /// <summary>
    /// Creates an <see cref="Result{TValue}"/> from a list of errors.
    /// </summary>
    public static implicit operator Result<TValue>(List<Error> errors)
    {
        return new Result<TValue>(errors);
    }

    /// <summary>
    /// Creates an <see cref="Result{TValue}"/> from a list of errors.
    /// </summary>
    public static implicit operator Result<TValue>(Error[] errors)
    {
        return new Result<TValue>(errors.ToList());
    }

    public void Switch(Action<TValue> onValue, Action<List<Error>> onError)
    {
        if (IsFailure)
        {
            onError(Errors);
            return;
        }

        onValue(Value);
    }

    public async Task SwitchAsync(Func<TValue, Task> onValue, Func<List<Error>, Task> onError)
    {
        if (IsFailure)
        {
            await onError(Errors).ConfigureAwait(false);
            return;
        }

        await onValue(Value).ConfigureAwait(false);
    }

    public void SwitchFirst(Action<TValue> onValue, Action<Error> onFirstError)
    {
        if (IsFailure)
        {
            onFirstError(FirstError);
            return;
        }

        onValue(Value);
    }

    public async Task SwitchFirstAsync(Func<TValue, Task> onValue, Func<Error, Task> onFirstError)
    {
        if (IsFailure)
        {
            await onFirstError(FirstError).ConfigureAwait(false);
            return;
        }

        await onValue(Value).ConfigureAwait(false);
    }

    public TResult Match<TResult>(Func<TValue, TResult> onValue, Func<List<Error>, TResult> onError)
    {
        if (IsFailure)
        {
            return onError(Errors);
        }

        return onValue(Value);
    }

    public async Task<TResult> MatchAsync<TResult>(Func<TValue, Task<TResult>> onValue, Func<List<Error>, Task<TResult>> onError)
    {
        if (IsFailure)
        {
            return await onError(Errors).ConfigureAwait(false);
        }

        return await onValue(Value).ConfigureAwait(false);
    }

    public TResult MatchFirst<TResult>(Func<TValue, TResult> onValue, Func<Error, TResult> onFirstError)
    {
        if (IsFailure)
        {
            return onFirstError(FirstError);
        }

        return onValue(Value);
    }

    public async Task<TResult> MatchFirstAsync<TResult>(Func<TValue, Task<TResult>> onValue, Func<Error, Task<TResult>> onFirstError)
    {
        if (IsFailure)
        {
            return await onFirstError(FirstError).ConfigureAwait(false);
        }

        return await onValue(Value).ConfigureAwait(false);
    }
}

public readonly record struct Success;
public readonly record struct Created;
public readonly record struct Deleted;
public readonly record struct Updated;