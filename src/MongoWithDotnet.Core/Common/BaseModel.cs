using MongoDB.Bson.Serialization.Attributes;

namespace MongoWithDotnet.Core.Common;

/// <summary>
/// Base model
/// Must inheritance from BaseEntity, IHasCreationTime, IHasUpdateDate, IEpochTime, IOrderNo, IVisible to define common properties
/// </summary>
public class BaseModel : BaseEntity, IHasCreationTime, IHasUpdateDate, IEpochTime, IOrderNo, IVisible
{
    [BsonElement("createDate")] public string CreateDate { get; set; } = DateTime.Now.ToString("h:mm:ss tt zz");

    [BsonElement("updateDate")] public string UpdateDate { get; set; } = DateTime.Now.ToString("h:mm:ss tt zz");

    [BsonElement("epochTime")] public long EpochTime { get; set; } = 0;

    [BsonElement("orderNo")] public int OrderNo { get; set; } = 0;

    [BsonElement("visible")] public bool IsVisible { get; set; } = true;
}