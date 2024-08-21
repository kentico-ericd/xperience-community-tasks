using Microsoft.Extensions.DependencyInjection;

using Xperience.Community.Tasks.Repositories;
using Xperience.Community.Tasks.Services;

namespace Xperience.Community.Tasks;

/// <summary>
/// Contains methods for initializing the Xperience tasks integration.
/// </summary>
public static class StartupExtensions
{
    /// <summary>
    /// Registers required task services and all <see cref="IXperienceTask"/> implementations.
    /// </summary>
    public static IServiceCollection AddKenticoTasks(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var tasks = assemblies.SelectMany(a => a.GetTypes()).Where(t =>
                        t.IsClass
                        && !t.IsAbstract
                        && typeof(IXperienceTask).IsAssignableFrom(t));
        foreach (var task in tasks)
        {
            services.AddSingleton(typeof(IXperienceTask), task);
        }

        services.AddSingleton<IXperienceTaskRunner, XperienceTaskRunner>();
        services.AddSingleton<IXperienceTaskRepository, XperienceTaskRepository>();
        services.AddSingleton<IXperienceTaskMetadataService, XperienceTaskMetadataService>();
        services.AddHostedService<XperienceTaskBackgroundService>();

        return services;
    }
}
