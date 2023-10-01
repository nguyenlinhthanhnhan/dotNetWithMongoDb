using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoWithDotnet.Application.Services.AuthenticationService;
using MongoWithDotnet.Application.Services.AuthenticationService.DTO;
using MongoWithDotnet.Application.Services.AuthorizationService;

namespace MongoWithDotnet.View.CRM.Controllers.v1;

[ApiVersion("1.0")]
public class AuthenticationController : ApiController
{
    private readonly IUserManager _userManager;
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IUserManager userManager, IAuthenticationService authenticationService)
    {
        _userManager = userManager;
        _authenticationService = authenticationService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] LoginRequestDto dto)
    {
        var response = await _authenticationService.Login(dto);

        if (response is { RefreshToken: null, AccessToken: null })
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserDto model)
    {
        var response = await _userManager.Create(model);

        return Ok(response);
    }

    // [HttpPost("refresh")]
    // public IActionResult Refresh([FromBody] RefreshTokenRequest model)
    // {
    //     var response = _userManager.RefreshToken(model);
    //
    //     if (response == null)
    //         return BadRequest(new { message = "Username or password is incorrect" });
    //
    //     return Ok(response);
    // }

    // [HttpPost("revoke")]
    // public IActionResult Revoke([FromBody] RevokeTokenRequest model)
    // {
    //     var response = _userManager.RevokeToken(model);
    //
    //     if (response == null)
    //         return BadRequest(new { message = "Username or password is incorrect" });
    //
    //     return Ok(response);
    // }
}