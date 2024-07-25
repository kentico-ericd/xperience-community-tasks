using CMS.Base;
using CMS.Core;

using Xperience.Labs.Tasks.Repositories;

namespace Xperience.Labs.Tasks;

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
        if (!taskRepository.ValidateTaskNames())
        {
            LogDuplicateTasks();

            return;
        }

        var tasks = taskRepository.GetTasksToRun();
        if (!tasks.Any())
        {
            return;
        }

        LogProcessStart(tasks);
        foreach (var task in tasks)
        {
            new CMSThread(new ThreadStart(() => RunTask(task))).RunAsync();
        }
    }

    private void LogDuplicateTasks()
    {
        string msg = "Cannot execute tasks with duplicate names. Worker thread will not execute any tasks until this error is corrected.";
        logService.LogError(nameof(XperienceTaskWorker), nameof(Process), msg);
        StopExecution();
    }

    private void LogProcessStart(IEnumerable<IXperienceTask> tasks)
    {
        string taskNames = string.Join(string.Empty, tasks.Select(t => $"\n - {t.Settings.Name}"));
        logService.LogInformation(nameof(XperienceTaskWorker), nameof(Process), $"Executing tasks:{taskNames}");
    }

    private void RunTask(IXperienceTask task)
    {
        try
        {
            taskRepository.SetNextRun(task, DateTime.Now.AddMinutes(task.Settings.IntervalMinutes));
            task.Execute();
        }
        catch (Exception ex)
        {
            logService.LogException(nameof(XperienceTaskWorker), nameof(RunTask), ex, $"Error running task '{task.Settings.Name}'");
        }
    }
}
