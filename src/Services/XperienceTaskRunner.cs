using CMS.Base;
using CMS.Core;

namespace Xperience.Labs.Tasks.Services;

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

    public void Run(IXperienceTask task)
    {
        try
        {
            var meta = metadataService.GetMetadata(task);
            meta.LastRun = DateTime.Now;
            meta.NextRun = DateTime.Now.AddMinutes(task.Settings.IntervalMinutes);
            meta.Executions++;
            new CMSThread(new ThreadStart(task.Execute)).RunAsync();
        }
        catch (Exception ex)
        {
            logService.LogException(nameof(XperienceTaskRunner), nameof(Run), ex, $"Error running task '{task.Settings.Name}'");
        }
    }
}
