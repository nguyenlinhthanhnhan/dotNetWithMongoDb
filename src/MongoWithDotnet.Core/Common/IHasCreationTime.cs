namespace MongoWithDotnet.Core.Common;

/// <summary>
/// Creation time of entity.
/// </summary>
public interface IHasCreationTime
{
    string CreateDate { get; set; }
}