namespace Xperience.Labs.Tasks.Repositories;

/// <summary>
/// Default implementation of <see cref="IXperienceTaskRepository"/>.
/// </summary>
internal class XperienceTaskRepository : IXperienceTaskRepository
{
    private readonly IEnumerable<IXperienceTask> tasks;
    private readonly Dictionary<string, DateTime> nextRuns = [];

    public XperienceTaskRepository(IEnumerable<IXperienceTask> tasks) => this.tasks = tasks;

    public void SetNextRun(IXperienceTask task, DateTime lastRun) => nextRuns[task.Settings.Name] = lastRun;

    public IEnumerable<IXperienceTask> GetTasksToRun() => tasks.Where(t =>
        (!t.Settings.ExecutionHours.Any() || t.Settings.ExecutionHours.Contains(DateTime.Now.Hour))
        && DateTime.Now >= GetNextRun(t)
        && t.ShouldExecute());

    public bool ValidateTaskNames() => tasks.DistinctBy(t => t.Settings.Name).Count() == tasks.Count();

    private DateTime GetNextRun(IXperienceTask task) => nextRuns.GetValueOrDefault(task.Settings.Name);
}
