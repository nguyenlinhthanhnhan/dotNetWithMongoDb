namespace MongoWithDotnet.Core.Common;

/// <summary>
/// Update time of entity.
/// </summary>
public interface IHasUpdateDate
{
    string UpdateDate { get; set; }
}