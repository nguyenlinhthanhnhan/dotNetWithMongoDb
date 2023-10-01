namespace MongoWithDotnet.Application.Services.AuthenticationService.DTO;

public class GrantRoleToUserDto
{
    public string UserId { get; set; }
    public string RoleId { get; set; }
}