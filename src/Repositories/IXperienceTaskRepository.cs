namespace Labs.Kentico.Xperience.Tasks.Repositories;

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
    void SetNextRun(IXperienceTask task, DateTime lastRun);
}
