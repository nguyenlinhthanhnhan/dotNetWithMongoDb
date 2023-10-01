using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MongoWithDotnet.View.CRM.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class PermissionAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private string[] Permissions { get; }

    public PermissionAuthorizeAttribute(params string[] permissions)
    {
        Permissions = permissions;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Headers.Authorization.FirstOrDefault();

        if (string.IsNullOrEmpty(token))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var handler = new JwtSecurityTokenHandler();
        var decodedToken = handler.ReadToken(token) as JwtSecurityToken;

        // Element at will change depending on order of claims in token
        var listPermissions = decodedToken?.Claims.ElementAt(1).Value.Split(",").ToList();

        // If Permissions not all in listPermissions
        if (Permissions.Any(permission => listPermissions != null && !listPermissions.Contains(permission)))
            context.Result = new UnauthorizedResult();
    }
}