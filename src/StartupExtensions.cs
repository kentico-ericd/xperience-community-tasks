using Xperience.Labs.Tasks.Repositories;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Xperience.Labs.Tasks;

/// <summary>
/// Contains methods for initializing the Xperience tasks integration.
/// </summary>
public static class StartupExtensions
{
    /// <summary>
    /// Ensures that the task worker thread is running.
    /// </summary>
    public static IApplicationBuilder StartKenticoTasks(this IApplicationBuilder app)
    {
        XperienceTaskWorker.Current.EnsureRunningThread();

        return app;
    }

    /// <summary>
    /// Registers required task services and all <see cref="IXperienceTask"/> implementations.
    /// </summary>
    public static IServiceCollection AddKenticoTasks(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var types = assemblies.SelectMany(a => a.GetTypes()).Where(t =>
                        t.IsClass
                        && !t.IsAbstract
                        && typeof(IXperienceTask).IsAssignableFrom(t));
        foreach (var implementationType in types)
        {
            foreach (var interfaceType in implementationType.GetInterfaces())
            {
                services.AddSingleton(interfaceType, implementationType);
            }
        }

        services.AddSingleton<IXperienceTaskRepository, XperienceTaskRepository>();

        return services;
    }
}
