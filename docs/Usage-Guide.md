# Usage guide

## Task settings

When you create a new task, you must supply the `Settings` property which controls how/when the task executes. You may set the following properties:

- __Name__ (required): An arbitrary string identifying the task
- __IntervalMinutes__ (required): The number of minutes between each task execution
- __ExecutionHours__: The hours of the day which the task is allowed to execute. If not set, the task executes at any hour

```cs
// A task that executes all day every 5 minutes
public XperienceTaskSettings Settings => new("Clear temporary files", 5);

// A task that executes every hour between 11PM-1AM
public XperienceTaskSettings Settings => new("Synchronize products", 60) {
    ExecutionHours = new int[] { 23, 0 }
};
```

## Dependency injection

Tasks support constructor injection. For example, if you need to log information about the task's execution, you can inject an instance of `IEventLogService`:

```cs
public class MyTask : IXperienceTask
{
    private readonly IEventLogService eventLogService;

    public XperienceTaskSettings Settings => new(nameof(MyTask), 1);

    public MyTask(IEventLogService eventLogService) => this.eventLogService = eventLogService;

    public void Execute() => eventLogService.LogWarning(nameof(MyTask), nameof(Execute), "I ran");
}
```

## Custom scheduling

Aside from setting the [execution hours](#task-settings), you may also customize task execution within the `IXperienceTask.ShouldExecute` method. This method is checked after a task has already been approved for execution based on its interval.

For standard tasks that should always execute, you can just return `true`:

```cs
public bool ShouldExecute() => true;
```

However, you may wish to validate the current execution time manually. Or, you could use a [custom setting](https://docs.kentico.com/guides/development/customizations-and-integrations/create-basic-module) or application setting to enable your task. You can check these values in `ShouldExecute`:

```cs
public class MyTask : IXperienceTask
{
    private readonly ISettingsService settingsService;

    public XperienceTaskSettings Settings => new(nameof(MyTask), 1);

    public MyTask(ISettingsService settingsService) => this.settingsService = settingsService;

    public void Execute()
    {
        // Do something...
    }

    public bool ShouldExecute() => ValidationHelper.GetBoolean(settingsService["MyTaskEnabled"], true);
}
```

## Administration UI

There is a new __Tasks__ application that can be found under the __Development__ category. Within the application, you can view a list of registered tasks as well as their configuration:

![Task listing](/images/ui.png)

In each row, there is an "Execute" action which will run the task immediately. The task will run regardless of its scheduled next run time and allowed hours.
