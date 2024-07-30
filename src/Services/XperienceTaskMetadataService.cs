using Xperience.Labs.Tasks.Tasks;

namespace Xperience.Labs.Tasks.Services;

/// <summary>
/// Default implementation of <see cref="IXperienceTaskMetadataService"/>.
/// </summary>
internal class XperienceTaskMetadataService : IXperienceTaskMetadataService
{
    private readonly Dictionary<string, XperienceTaskMetadata> metadata = [];

    public XperienceTaskMetadata GetMetadata(IXperienceTask task)
    {
        var meta = metadata.GetValueOrDefault(task.Settings.Name);
        if (meta is null)
        {
            meta = new XperienceTaskMetadata()
            {
                NextRun = DateTime.Now.AddMinutes(task.Settings.IntervalMinutes)
            };
            metadata[task.Settings.Name] = meta;
        }

        return meta;
    }
}
