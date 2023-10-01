using MongoWithDotnet.Core.Authorization;

namespace MongoWithDotnet.DataAccess.Authorization;

public class ApplicationAuthorizationProvider : AuthorizationProvider
{
    public override void SetPermissions(IPermissionDefinitionContext context)
    {
        var admin = context.CreatePermission(PermissionNames.Pages_Administration);
        admin.CreateChildPermission(PermissionNames.Pages_Administration_Users);
        admin.CreateChildPermission(PermissionNames.Pages_Administration_Users_Activation);
    }
}