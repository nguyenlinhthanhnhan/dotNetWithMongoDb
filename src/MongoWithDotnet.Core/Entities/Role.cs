using MongoWithDotnet.Core.Common;

namespace MongoWithDotnet.Core.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; }
}