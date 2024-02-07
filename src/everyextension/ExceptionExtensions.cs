using System.Diagnostics;

namespace EveryExtension;

public static class ExceptionExtensions
{
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

    public static string CaptureStackTrace(this Exception exception)
        => new StackTrace(exception).ToString();

    public static Exception Wrap(this Exception exception, string additionalContext)
    {
        string message = $"{additionalContext} - {exception.Message}";
        return new Exception(message, exception);
    }
}
