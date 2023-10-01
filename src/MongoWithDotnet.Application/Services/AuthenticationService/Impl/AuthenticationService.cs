using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MongoWithDotnet.Application.Exceptions;
using MongoWithDotnet.Application.Helpers;
using MongoWithDotnet.Application.Services.AuthenticationService.DTO;
using MongoWithDotnet.Application.Services.AuthorizationService;
using MongoWithDotnet.Core.Entities;
using MongoWithDotnet.DataAccess.Repositories;

namespace MongoWithDotnet.Application.Services.AuthenticationService.Impl;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserManager _userManager;
    private readonly IRoleManager _roleManager;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IConfiguration _configuration;

    public AuthenticationService(IUserManager userManager,
        IRoleManager roleManager, IPermissionRepository permissionRepository, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _permissionRepository = permissionRepository;
        _configuration = configuration;
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var userFilter = Builders<User>.Filter.Eq(x => x.Name, loginRequestDto.Name);
        var user = await _userManager.GetFirst(userFilter);

        if (user == null) throw new BadRequestException("Username or password is incorrect");

        // WARNING: This is a security vulnerability. Do not use this code in production.
        // Mush hash the password and compare the hashes.
        if (user.Password != loginRequestDto.Password)
            throw new BadRequestException("Username or password is incorrect");

        var roleFilter = Builders<Role>.Filter.Eq(x => x.Id, user.RoleId);
        var role = await _roleManager.GetFirst(roleFilter);

        var permissionsFilter = Builders<ApplicationPermission>.Filter.Eq(x => x.RoleId, role.Id);
        var permissions = (await _permissionRepository.GetAllAsync(permissionsFilter)).ToArray();

        var accessToken = JwtHelper.GenerateAccessToken(new GenerateUserTokenDto
        {
            Id = user.Id,
            Permissions = permissions
        }, _configuration);

        var refreshToken = JwtHelper.GenerateRefreshToken();

        return new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    // Test login
    public async Task<LoginResponseDto> LoginUserAsync(LoginRequestDto loginRequestDto)
    {
        var userFilter = Builders<User>.Filter.Eq(x => x.Name, loginRequestDto.Name);
        var user = await _userManager.GetFirst(userFilter);

        if (user == null) throw new BadRequestException("Username or password is incorrect");

        // WARNING: This is a security vulnerability. Do not use this code in production.
        // Mush hash the password and compare the hashes.
        if (user.Password != loginRequestDto.Password)
            throw new BadRequestException("Username or password is incorrect");

        var claims = new[]
        {
            new Claim("Email", user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfiguration:SecretKey"]));
        var token = new JwtSecurityToken(
            _configuration["JwtConfiguration:ValidIssuer"],
            _configuration["JwtConfiguration:ValidAudience"],
            claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );
        var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

        return new LoginResponseDto
        {
            AccessToken = tokenAsString,
            RefreshToken = JwtHelper.GenerateRefreshToken()
        };
    }
}