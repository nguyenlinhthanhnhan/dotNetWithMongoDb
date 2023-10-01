namespace MongoWithDotnet.Application.Services.AuthenticationService.DTO;

public class GrantPermissionToRoleDto
{
    public string RoleName { get; set; }

    public string[] Permissions { get; set; }
}