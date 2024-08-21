using System.Diagnostics;

using CMS.Core;
using CMS.DataEngine;

using XperienceCommunity.Tasks.Repositories;
using XperienceCommunity.Tasks.Services;

namespace XperienceCommunity.Tasks;

public class XperienceTaskBackgroundService : ApplicationBackgroundService
{
    private readonly IEventLogService logService;
    private readonly IXperienceTaskRunner taskRunner;
    private readonly IXperienceTaskRepository taskRepository;

    public XperienceTaskBackgroundService(
        IEventLogService logService,
        IXperienceTaskRunner taskRunner,
        IXperienceTaskRepository taskRepository)
    {
        this.logService = logService;
        this.taskRunner = taskRunner;
        this.taskRepository = taskRepository;

        ShouldRestart = true;
        RestartDelay = 60000;
    }

    protected override async Task ExecuteInternal(CancellationToken stoppingToken)
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
            await taskRunner.Run(task);
        }
    }

    private void LogDuplicateTasks()
    {
        string msg = "Cannot execute tasks with duplicate names. No tasks will execute until this error is corrected.";
        logService.LogError(nameof(XperienceTaskBackgroundService), nameof(Process), msg);
    }

    private void LogProcessStart(IEnumerable<IXperienceTask> tasks)
    {
        string taskNames = string.Join(string.Empty, tasks.Select(t => $"\n - {t.Settings.Name}"));
        logService.LogInformation(nameof(XperienceTaskBackgroundService), nameof(Process), $"Executing tasks:{taskNames}");
    }
}
