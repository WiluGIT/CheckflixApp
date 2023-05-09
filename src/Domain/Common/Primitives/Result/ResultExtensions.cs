namespace CheckflixApp.Domain.Common.Primitives.Result;

/// <summary>
/// Contains extension methods for the result class.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Ensures that the specified predicate is true, otherwise adds a failure result to error list.
    /// </summary>
    /// <typeparam name = "List<Error>" > The result type.</typeparam>
    /// <param name = "errorResult" > The error result list.</param>
    /// <param name = "predicate" > The predicate.</param>
    /// <param name = "error" > The error.</param>
    /// <returns>
    /// The current error list result if the predicate is true, otherwise adds error to the list.
    /// </returns>
    //public static List<Error> Ensure<T>(this List<Error> errorResult, T value, Func<T, bool> predicate, Error error)
    public static List<Error> Ensure(this List<Error> errorResult, bool predicateValue, Error error)
    {
        if (!predicateValue)
        {
            errorResult.Add(error);
        }

        return errorResult;
    }

    /// <summary>
    /// Binds to the result of the function and returns it.
    /// </summary>
    /// <typeparam name="TIn">The result type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>
    /// The success result with the bound value if the current result is a success result, otherwise a failure result.
    /// </returns>
    public static async Task<Result<TOut>> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Task<Result<TOut>>> func) =>
        !result.IsFailure ? await func(result.Value) : result.Errors;

    /// <summary>
    /// Matches to the corresponding functions based on existence of the value.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <typeparam name="TOut">The output type.</typeparam>
    /// <param name="resultTask">The maybe task.</param>
    /// <param name="onSuccess">The on-success function.</param>
    /// <param name="onFailure">The on-failure function.</param>
    /// <returns>
    /// The result of the on-success function if the maybe has a value, otherwise the result of the failure result.
    /// </returns>
    public static async Task<TOut> Match<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, TOut> onSuccess, Func<List<Error>, TOut> onError)
    {
        Result<TIn> result = await resultTask;

        return !result.IsFailure ? onSuccess(result.Value) : onError(result.Errors);
    }
}