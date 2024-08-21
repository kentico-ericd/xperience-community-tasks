using Xperience.Community.Tasks.Services;

namespace Xperience.Community.Tasks.Repositories;

/// <summary>
/// Default implementation of <see cref="IXperienceTaskRepository"/>.
/// </summary>
internal class XperienceTaskRepository : IXperienceTaskRepository
{
    private readonly IEnumerable<IXperienceTask> tasks;
    private readonly IXperienceTaskMetadataService metadataService;

    public XperienceTaskRepository(IEnumerable<IXperienceTask> tasks, IXperienceTaskMetadataService metadataService)
    {
        this.tasks = tasks;
        this.metadataService = metadataService;
    }

    public IEnumerable<IXperienceTask> GetTasks() => tasks;

    public IEnumerable<IXperienceTask> GetTasksToRun() => tasks.Where(t =>
        (!t.Settings.ExecutionHours.Any() || t.Settings.ExecutionHours.Contains(DateTime.Now.Hour))
        && DateTime.Now >= metadataService.GetMetadata(t).NextRun
        && t.ShouldExecute());

    public bool ValidateTaskNames() => tasks.DistinctBy(t => t.Settings.Name).Count() == tasks.Count();
}
