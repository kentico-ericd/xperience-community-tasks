namespace XperienceCommunity.Tasks.Services;

/// <summary>
/// Contains methods for executing tasks.
/// </summary>
public interface IXperienceTaskRunner
{
    /// <summary>
    /// Executes the task without validation. Validation of whether a task should be executed should be performed before calling this
    /// method.
    /// </summary>
    Task Run(IXperienceTask task, CancellationToken cancellationToken);
}
