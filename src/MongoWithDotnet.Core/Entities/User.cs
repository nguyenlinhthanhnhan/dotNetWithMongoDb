using MongoWithDotnet.Core.Common;

namespace MongoWithDotnet.Core.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }

    public string? Email { get; set; }

    public string Password { get; set; }

    public string RoleId { get; set; }
}