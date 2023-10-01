using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoWithDotnet.Core.Common;

/// <summary>
/// Base of Entity
/// Entity is re-presented for a collection in the database ( table in SQL, collection in MongoDB, in this case, we use MongoDb )
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// MongoDb Id, named "_id" in MongoDb
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
}