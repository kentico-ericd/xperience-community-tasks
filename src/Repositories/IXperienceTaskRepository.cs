namespace XperienceCommunity.Tasks.Repositories;

/// <summary>
/// Contains methods for getting tasks and managing execution times.
/// </summary>
public interface IXperienceTaskRepository
{
    /// <summary>
    /// Gets all tasks.
    /// </summary>
    IEnumerable<IXperienceTask> GetTasks();

    /// <summary>
    /// Gets all tasks that should execute at the current time.
    /// </summary>
    IEnumerable<IXperienceTask> GetTasksToRun();

    /// <summary>
    /// Returns <c>false</c> if registered task names are invalid and should not be processed.
    /// </summary>
    bool ValidateTaskNames();
}
