using MongoWithDotnet.Application.Services.AuthenticationService.DTO;

namespace MongoWithDotnet.Application.Services.AuthenticationService;

public interface IAuthenticationService
{
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

    Task<LoginResponseDto> LoginUserAsync(LoginRequestDto loginRequestDto);
}