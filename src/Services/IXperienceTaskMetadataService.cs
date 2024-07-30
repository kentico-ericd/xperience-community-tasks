using Xperience.Labs.Tasks.Tasks;

namespace Xperience.Labs.Tasks.Services;

/// <summary>
/// Contains methods for managing task metadata.
/// </summary>
public interface IXperienceTaskMetadataService
{
    /// <summary>
    /// Gets task metadata.
    /// </summary>
    XperienceTaskMetadata GetMetadata(IXperienceTask task);
}
