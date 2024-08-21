using CMS.Membership;

using Kentico.Xperience.Admin.Base;

using XperienceCommunity.Tasks.Admin;
using XperienceCommunity.Tasks.Repositories;
using XperienceCommunity.Tasks.Services;

using Action = Kentico.Xperience.Admin.Base.Action;

[assembly: UIPage(
   parentType: typeof(TaskApplicationPage),
   slug: "list",
   uiPageType: typeof(TaskListingPage),
   name: "List of tasks",
   templateName: TemplateNames.LISTING,
   order: UIPageOrder.First)]

namespace XperienceCommunity.Tasks.Admin;

/// <summary>
/// An admin UI page that displays statistics about the registered Xperience tasks.
/// </summary>
[UIEvaluatePermission(SystemPermissions.VIEW)]
internal class TaskListingPage : ListingPageBase<ListingConfiguration>
{
    private ListingConfiguration? listingConfiguration;
    private readonly IXperienceTaskRunner taskRunner;
    private readonly IXperienceTaskRepository taskRepository;
    private readonly IXperienceTaskMetadataService metadataService;

    public override ListingConfiguration PageConfiguration
    {
        get
        {
            listingConfiguration ??= new ListingConfiguration()
            {
                Caption = "Tasks",
                Callouts = [],
                MassActions = [],
                TableActions = [],
                HeaderActions = [],
                ColumnConfigurations = [],
                PageSizes = new List<int> { 10, 25 }
            };

            return listingConfiguration;

        }
        set => listingConfiguration = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskListingPage"/> class.
    /// </summary>
    public TaskListingPage(IXperienceTaskRunner taskRunner, IXperienceTaskRepository taskRepository, IXperienceTaskMetadataService metadataService)
    {
        this.taskRunner = taskRunner;
        this.taskRepository = taskRepository;
        this.metadataService = metadataService;
    }

    [PageCommand]
    public Task<ICommandResponse<RowActionResult>> Execute(int id, CancellationToken _)
    {
        var task = taskRepository.GetTasks().FirstOrDefault(t => t.GetHashCode() == id);
        if (task is null)
        {
            return Task.FromResult(ResponseFrom(new RowActionResult(false)).AddErrorMessage("Failed to retrieve task."));
        }

        taskRunner.Run(task);

        return Task.FromResult(ResponseFrom(new RowActionResult(true)).AddInfoMessage($"Task executed successfully."));
    }

    public override async Task ConfigurePage()
    {
        if (!taskRepository.GetTasks().Any())
        {
            PageConfiguration.Callouts =
            [
                new()
                {
                    Headline = "No tasks",
                    Content = "No tasks registered. See <a target='_blank' href='https://github.com/kentico-ericd/xperience-by-kentico-tasks'>our instructions</a> to read more about creating tasks.",
                    ContentAsHtml = true,
                    Type = CalloutType.FriendlyWarning,
                    Placement = CalloutPlacement.OnDesk
                }
            ];
        }

        PageConfiguration.TableActions.AddCommand("Execute", nameof(Execute));
        PageConfiguration.ColumnConfigurations
            .AddColumn("Name", "Name", sortable: false)
            .AddColumn("LastRun", "Last run", sortable: false)
            .AddColumn("Interval", "Interval (minutes)", sortable: false)
            .AddColumn("Hours", "Allowed hours", sortable: false)
            .AddColumn("Executions", "Executions", sortable: false);

        await base.ConfigurePage();
    }

    protected override Task<LoadDataResult> LoadData(LoadDataSettings settings, CancellationToken cancellationToken)
    {
        var rows = taskRepository.GetTasks().Select(GetRow);

        return Task.FromResult(new LoadDataResult
        {
            Rows = rows,
            TotalCount = rows.Count()
        });
    }

    private Row GetRow(IXperienceTask task)
    {
        var meta = metadataService.GetMetadata(task);

        return new()
        {
            Identifier = task.GetHashCode(),
            Cells = new List<Cell>
            {
                new StringCell
                {
                    Value = task.Settings.Name
                },
                new StringCell
                {
                    Value = meta.LastRun == DateTime.MinValue ? "-" : meta.LastRun.ToString()
                },
                new StringCell
                {
                    Value = task.Settings.IntervalMinutes.ToString()
                },
                new StringCell
                {
                    Value = task.Settings.ExecutionHours.Any() ? string.Join(", ", task.Settings.ExecutionHours) : "All day"
                },
                new StringCell
                {
                    Value = meta.Executions.ToString()
                },
                new ActionCell
                {
                    Actions = new List<Action>
                    {
                        new(ActionType.Command)
                        {
                            Title = "Execute",
                            Label = "Execute",
                            Icon = Icons.ChevronRightCircle,
                            ButtonColor = ButtonColor.Secondary,
                            Parameter = nameof(Execute)
                        }
                    }
                }
            }
        };
    }
}
