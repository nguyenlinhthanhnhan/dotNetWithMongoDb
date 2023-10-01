using System.Collections.Immutable;
using MongoWithDotnet.Core.Authorization;
using MongoWithDotnet.Shared.Collections.Extensions;

namespace MongoWithDotnet.DataAccess.Authorization;

public sealed class PermissionManager : PermissionDefinitionContextBase, IPermissionManager
{
    public PermissionManager()
    {
        Initialize();
    }

    private void Initialize()
    {
        var provider = new ApplicationAuthorizationProvider();
        provider.SetPermissions(this);
        Permissions.AddAllPermissions();
    }

    public Permission GetPermission(string name)
    {
        var permission = Permissions.GetOrDefault(name);
        if (permission == null) throw new ArgumentException($"There is no permission with name: {name}");

        return permission;
    }

    public IReadOnlyList<Permission> GetAllPermissions()
    {
        return Permissions.Values.ToImmutableList();
    }
}