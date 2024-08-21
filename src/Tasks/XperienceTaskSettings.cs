namespace Xperience.Community.Tasks;

/// <summary>
/// Represents the configuration of an individual <see cref="IXperienceTask"/>.
/// </summary>
public class XperienceTaskSettings
{
    /// <summary>
    /// The task name.
    /// </summary>
    public string Name { get; internal set; }

    /// <summary>
    /// How often the task should run, in minutes.
    /// </summary>
    public int IntervalMinutes { get; internal set; }

    /// <summary>
    /// The hours of the day (on a 24-hour clock) in which the task will execute.
    /// </summary>
    /// <remarks>Primarily used to set task execution during off-peak hours only, ie [22,23,0,1,2,3]. Accepted values are
    /// 0-23.</remarks>
    public IEnumerable<int> ExecutionHours { get; set; } = [];

    /// <summary>
    /// Initializes a new instance of <see cref="XperienceTaskSettings"/>.
    /// </summary>
    /// <param name="name">The task name.</param>
    /// <param name="intervalMinutes">How often the task should run, in minutes.</param>
    public XperienceTaskSettings(string name, int intervalMinutes)
    {
        Name = name;
        IntervalMinutes = intervalMinutes;
    }
}
