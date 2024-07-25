namespace Xperience.Labs.Tasks.Repositories;

/// <summary>
/// Contains methods for getting tasks and managing execution times.
/// </summary>
internal interface IXperienceTaskRepository
{
    /// <summary>
    /// Gets all tasks that should execute at the current time.
    /// </summary>
    IEnumerable<IXperienceTask> GetTasksToRun();

    /// <summary>
    /// Sets the time that the task should execute next.
    /// </summary>
    void SetNextRun(IXperienceTask task, DateTime nextRun);

    /// <summary>
    /// Returns <c>false</c> if registered task names are invalid and should not be processed.
    /// </summary>
    bool ValidateTaskNames();
}
