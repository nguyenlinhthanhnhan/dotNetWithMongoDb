using MongoWithDotnet.Core.Entities;

namespace MongoWithDotnet.Application.Services.AuthenticationService.DTO;

public class GenerateUserTokenDto
{
    public string Id { get; set; }

    public ApplicationPermission[] Permissions { get; set; }
}