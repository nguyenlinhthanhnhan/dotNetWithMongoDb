namespace MongoWithDotnet.Application.Services.AuthenticationService.DTO;

public class LoginResponseDto
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}