using System.Diagnostics;

namespace EveryExtension;

/// <summary>
/// Provides extension methods for working with Exceptions.
/// </summary>
public static class ExceptionExtensions
{
    /// <summary>
    /// Gets the full message including messages from inner exceptions.
    /// </summary>
    /// <param name="exception">The Exception object.</param>
    /// <returns>The full message including messages from inner exceptions.</returns>
    public static string GetFullMessage(this Exception exception)
    {
        if (exception == null)
            throw new ArgumentNullException(nameof(exception));

        string fullMessage = exception.Message;
        var innerException = exception.InnerException;

        while (innerException != null)
        {
            fullMessage += $" {innerException.Message}";
            innerException = innerException.InnerException;
        }

        return fullMessage;
    }

    /// <summary>
    /// Flattens the exception hierarchy into a sequence of exceptions.
    /// </summary>
    /// <param name="exception">The Exception object.</param>
    /// <returns>A sequence of exceptions including the original exception and inner exceptions.</returns>
    public static IEnumerable<Exception> Flatten(this Exception exception)
    {
        if (exception == null)
            throw new ArgumentNullException(nameof(exception));

        yield return exception;

        if (exception is AggregateException aggregateException)
        {
            foreach (var innerException in aggregateException.InnerExceptions)
            {
                foreach (var flattenedException in innerException.Flatten())
                {
                    yield return flattenedException;
                }
            }
        }
        else if (exception.InnerException != null)
        {
            foreach (var flattenedException in exception.InnerException.Flatten())
            {
                yield return flattenedException;
            }
        }
    }

    /// <summary>
    /// Captures the stack trace of the exception.
    /// </summary>
    /// <param name="exception">The Exception object.</param>
    /// <returns>The captured stack trace as a string.</returns>
    public static string CaptureStackTrace(this Exception exception)
        => new StackTrace(exception).ToString();

    /// <summary>
    /// Wraps the exception with additional context in the message.
    /// </summary>
    /// <param name="exception">The Exception object to wrap.</param>
    /// <param name="additionalContext">Additional context to include in the wrapped exception message.</param>
    /// <returns>The wrapped exception.</returns>
    public static Exception Wrap(this Exception exception, string additionalContext)
    {
        string message = $"{additionalContext} - {exception.Message}";
        return new Exception(message, exception);
    }
}
