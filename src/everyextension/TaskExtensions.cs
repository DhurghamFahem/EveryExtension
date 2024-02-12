namespace EveryExtension;

/// <summary>
/// Provides extension methods for working with Tasks.
/// </summary>
public static class TaskExtensions
{
    /// <summary>
    /// Adds a timeout to a Task, throwing a TimeoutException if the operation takes too long.
    /// </summary>
    /// <typeparam name="TResult">The type of the result produced by the task.</typeparam>
    /// <param name="task">The Task to which a timeout is applied.</param>
    /// <param name="timeout">The maximum duration allowed for the task to complete.</param>
    /// <returns>A Task representing the original task with a timeout.</returns>
    public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, TimeSpan timeout)
    {
        using var timeoutCancellationTokenSource = new CancellationTokenSource();
        var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));
        if (completedTask == task)
        {
            timeoutCancellationTokenSource.Cancel();
            return await task;
        }
        throw new TimeoutException("The operation has timed out.");
    }

    /// <summary>
    /// Adds a cancellation token to a Task, allowing for cancellation during execution.
    /// </summary>
    /// <typeparam name="T">The type of the result produced by the task.</typeparam>
    /// <param name="task">The Task to which a cancellation token is added.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task representing the original task with a cancellation token.</returns>
    public static Task<T> WithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
    {
        return task.IsCompleted
            ? task
            : task.ContinueWith(
                completedTask => completedTask.GetAwaiter().GetResult(),
                cancellationToken,
                TaskContinuationOptions.ExecuteSynchronously,
                TaskScheduler.Default
            );
    }

    /// <summary>
    /// Adds a cancellation token to a Task, allowing for cancellation during execution.
    /// </summary>
    /// <param name="task">The Task to which a cancellation token is added.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task representing the original task with a cancellation token.</returns>
    public static Task WithCancellation(this Task task, CancellationToken cancellationToken)
    {
        return task.IsCompleted
            ? task
            : task.ContinueWith(
                completedTask => completedTask.GetAwaiter().GetResult(),
                cancellationToken,
                TaskContinuationOptions.ExecuteSynchronously,
                TaskScheduler.Default
            );
    }
}
