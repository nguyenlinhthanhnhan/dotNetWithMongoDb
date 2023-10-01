namespace MongoWithDotnet.Core.Common;

/// <summary>
/// Visibility of entity.
/// </summary>
public interface IVisible
{
    bool IsVisible { get; set; }
}