using MongoWithDotnet.Core.Common;

namespace MongoWithDotnet.Core.Entities;

/// <summary>
/// Used to grant/deny a permission for a role or user.
/// </summary>
public class ApplicationPermission : BaseEntity
{
    /// <summary>
    /// Unique name of the permission.
    /// </summary>
    public virtual string Name { get; set; }

    /// <summary>
    /// Is this role granted for this permission.
    /// Default value: true.
    /// </summary>
    public virtual bool IsGranted { get; set; }

    /// <summary>
    /// Role Id
    /// </summary>
    public virtual string? RoleId { get; set; }

    public ApplicationPermission()
    {
        IsGranted = true;
    }
}