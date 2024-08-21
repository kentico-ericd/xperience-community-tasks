namespace XperienceCommunity.Tasks;

/// <summary>
/// Represents an Xperience by Kentico task which runs in the background on a timer.
/// </summary>
public interface IXperienceTask
{
    /// <summary>
    /// The task configuration.
    /// </summary>
    XperienceTaskSettings Settings { get; }

    /// <summary>
    /// Executes the task.
    /// </summary>
    Task Execute(CancellationToken cancellationToken);

    /// <summary>
    /// Called before the task executes.
    /// </summary>
    /// <returns><c>True</c> if the task should execute (e.g. if it is enabled).</returns>
    bool ShouldExecute();
}
