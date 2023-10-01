namespace MongoWithDotnet.Application.Services.AuthenticationService.DTO;

public class LoginRequestDto
{
    public string Name { get; set; }
    public string Password { get; set; }
}