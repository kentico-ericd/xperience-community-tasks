using Xperience.Community.Tasks.Tasks;

namespace Xperience.Community.Tasks.Services;

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
