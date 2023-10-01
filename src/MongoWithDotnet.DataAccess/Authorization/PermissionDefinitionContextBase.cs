using MongoWithDotnet.Core.Authorization;
using MongoWithDotnet.Shared.Collections.Extensions;

namespace MongoWithDotnet.DataAccess.Authorization;

public abstract class PermissionDefinitionContextBase : IPermissionDefinitionContext
{
    protected readonly PermissionDictionary Permissions;

    protected PermissionDefinitionContextBase()
    {
        Permissions = new PermissionDictionary();
    }

    public Permission CreatePermission(string name, string? displayName = null, string? description = null,
        Dictionary<string, object>? properties = null)
    {
        if (Permissions.ContainsKey(name))
            throw new ArgumentException($"There is already a permission with name: {name}");

        var permission = new Permission(name, displayName, description, properties);
        Permissions[permission.Name] = permission;
        return permission;
    }

    public Permission GetPermissionOrNull(string name)
    {
        return Permissions.GetOrDefault(name);
    }

    public void RemovePermission(string name)
    {
        Permissions.Remove(name);
    }
}