using MongoWithDotnet.Application.Services.AuthenticationService.DTO;
using MongoWithDotnet.Core.Authorization;

namespace MongoWithDotnet.Application.Services.AuthorizationService;

public interface IAuthorizationService
{
    Task<bool> GrantPermissionsToRole(GrantPermissionToRoleDto roleDto);

    Task<bool> GrantRoleToUser(GrantRoleToUserDto roleDto);

    string GetPermissions();
}