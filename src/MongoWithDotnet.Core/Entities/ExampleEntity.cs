using MongoDB.Bson.Serialization.Attributes;
using MongoWithDotnet.Core.Common;

namespace MongoWithDotnet.Core.Entities;

/// <summary>
/// This is an example
/// </summary>
public class ExampleEntity : BaseModel
{
    [BsonElement("name")] public string? Name { get; set; }
}