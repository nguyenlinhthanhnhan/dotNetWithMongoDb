namespace MongoWithDotnet.Core.Common;

/// <summary>
/// Epoch time of entity.
/// </summary>
public interface IEpochTime
{
    long EpochTime { get; set; }
}