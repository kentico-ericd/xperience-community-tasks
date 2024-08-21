using CMS.Core;

namespace XperienceCommunity.Tasks.Services;

/// <summary>
/// Default implementation of <see cref="IXperienceTaskRunner"/>.
/// </summary>
internal class XperienceTaskRunner : IXperienceTaskRunner
{
    private readonly IEventLogService logService;
    private readonly IXperienceTaskMetadataService metadataService;

    public XperienceTaskRunner(IEventLogService logService, IXperienceTaskMetadataService metadataService)
    {
        this.logService = logService;
        this.metadataService = metadataService;
    }

    public Task Run(IXperienceTask task, CancellationToken cancellationToken)
    {
        try
        {
            var meta = metadataService.GetMetadata(task);
            meta.LastRun = DateTime.Now;
            task.Execute(cancellationToken);
            meta.Executions++;
            meta.NextRun = DateTime.Now.AddMinutes(task.Settings.IntervalMinutes);
        }
        catch (Exception ex)
        {
            logService.LogException(nameof(XperienceTaskRunner), nameof(Run), ex, $"Error running task '{task.Settings.Name}'");
        }

        return Task.CompletedTask;
    }
}
