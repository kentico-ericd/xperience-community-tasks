using CMS.Base;
using CMS.Core;

using Kentico.Xperience.Tasks.Repositories;

namespace Kentico.Xperience.Tasks;

/// <summary>
/// Background thread worker which processes Xperience tasks.
/// </summary>
internal class XperienceTaskWorker : ThreadWorker<XperienceTaskWorker>
{
    private readonly IEventLogService logService;
    private readonly IXperienceTaskRepository taskRepository;

    protected override int DefaultInterval => 60000;

    public XperienceTaskWorker()
    {
        logService = Service.Resolve<IEventLogService>();
        taskRepository = Service.Resolve<IXperienceTaskRepository>();
    }

    protected override void Finish()
    {
    }

    protected override void Process()
    {
        var tasks = taskRepository.GetTasksToRun();
        if (!tasks.Any())
        {
            return;
        }

        LogProcessStart(tasks);
        foreach (var task in tasks)
        {
            taskRepository.SetNextRun(task, DateTime.Now.AddMinutes(task.Settings.IntervalMinutes));
            new CMSThread(new ThreadStart(() => task.Execute())).RunAsync();
        }
    }

    private void LogProcessStart(IEnumerable<IXperienceTask> tasks)
    {
        string taskNames = string.Join(string.Empty, tasks.Select(t => $"\n - {t.Settings.Name}"));
        logService.LogInformation(nameof(XperienceTaskWorker), nameof(Process), $"Executing tasks:{taskNames}");
    }
}
