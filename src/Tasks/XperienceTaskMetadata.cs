namespace XperienceCommunity.Tasks.Tasks;

/// <summary>
/// Represents individual in-memory task metadata
/// </summary>
public class XperienceTaskMetadata
{
    /// <summary>
    /// The task's last execution time.
    /// </summary>
    public DateTime LastRun { get; set; }

    /// <summary>
    /// The task's next execution time.
    /// </summary>
    public DateTime NextRun { get; set; }

    /// <summary>
    /// The number of times the task has executed.
    /// </summary>
    public int Executions { get; set; }
}
