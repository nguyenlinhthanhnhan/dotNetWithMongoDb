using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoWithDotnet.Application.Services.AuthenticationService.DTO;
using MongoWithDotnet.Application.Services.AuthorizationService;
using MongoWithDotnet.Core.Authorization;
using MongoWithDotnet.View.CRM.Filters;
using IAuthorizationService = MongoWithDotnet.Application.Services.AuthorizationService.IAuthorizationService;

namespace MongoWithDotnet.View.CRM.Controllers.v1;

[ApiVersion("1.0")]
public class TestAuthorizationController : ApiController
{
    private readonly IRoleManager _roleManager;
    private readonly IAuthorizationService _authorizationService;

    public TestAuthorizationController(IRoleManager roleManager, IAuthorizationService authorizationService)
    {
        _roleManager = roleManager;
        _authorizationService = authorizationService;
    }

    /// <summary>
    /// Create Role
    /// </summary>
    /// <param name="roleDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateRole(CreateRoleDto roleDto)
    {
        var result = await _roleManager.Create(roleDto);

        return Ok(result);
    }

    /// <summary>
    /// Get all permissions
    /// </summary>
    /// <returns></returns>
    [PermissionAuthorize(PermissionNames.Pages_Administration)]
    [HttpGet("/permissions")]
    public IActionResult GetAllPermissions()
    {
        var permissions = _authorizationService.GetPermissions();

        return Ok(permissions);
    }

    /// <summary>
    /// Grant permissions to role
    /// </summary>
    /// <param name="roleDto"></param>
    /// <returns></returns>
    [HttpPost("/grant-permission")]
    public async Task<IActionResult> GrantPermissionToRole(GrantPermissionToRoleDto roleDto)
    {
        var result = await _authorizationService.GrantPermissionsToRole(roleDto);

        return result ? Ok() : BadRequest();
    }

    /// <summary>
    /// Grant role to user
    /// </summary>
    /// <param name="roleDto"></param>
    /// <returns></returns>
    [HttpPost("/grant-role")]
    public async Task<IActionResult> GrantRoleToUser(GrantRoleToUserDto roleDto)
    {
        var result = await _authorizationService.GrantRoleToUser(roleDto);

        return result ? Ok() : BadRequest();
    }
}