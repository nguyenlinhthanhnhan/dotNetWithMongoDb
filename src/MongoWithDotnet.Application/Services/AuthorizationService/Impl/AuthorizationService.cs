using System.Text.Json;
using System.Text.Json.Serialization;
using MongoDB.Driver;
using MongoWithDotnet.Application.Exceptions;
using MongoWithDotnet.Application.Services.AuthenticationService.DTO;
using MongoWithDotnet.Core.Authorization;
using MongoWithDotnet.Core.Entities;
using MongoWithDotnet.DataAccess.Authorization;
using MongoWithDotnet.DataAccess.Repositories;

namespace MongoWithDotnet.Application.Services.AuthorizationService.Impl;

public class AuthorizationService : IAuthorizationService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionManager _permissionManager;
    private readonly IPermissionRepository _permissionRepository;

    public AuthorizationService(IUserRepository userRepository, IRoleRepository roleRepository,
        IPermissionManager permissionManager, IPermissionRepository permissionRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _permissionManager = permissionManager;
        _permissionRepository = permissionRepository;
    }

    public async Task<bool> GrantPermissionsToRole(GrantPermissionToRoleDto roleDto)
    {
        var roleFilter = Builders<Role>.Filter.Eq(x => x.Name, roleDto.RoleName);
        var role = await _roleRepository.GetFirstAsync(roleFilter);

        if (role == null)
            throw new NotFoundException("Role not found");

        // Consider check permissions is in list of all permissions

        // Consider check Role has permission

        var permissions = roleDto.Permissions
            .Select(permission => new ApplicationPermission { Name = permission, RoleId = role.Id }).ToList();

        var result = await _permissionRepository.InsertManyAsync(permissions);

        if (result.Count == 0)
            throw new BadRequestException("Permissions not granted");

        return true;
    }

    public async Task<bool> GrantRoleToUser(GrantRoleToUserDto roleDto)
    {
        var userFilter = Builders<User>.Filter.Eq(x => x.Id, roleDto.UserId);
        var user = await _userRepository.GetFirstAsync(userFilter);

        if (user == null)
            throw new NotFoundException("User not found");

        user.RoleId = roleDto.RoleId;

        var result = await _userRepository.UpdateAsync(user);

        if (result == null)
            throw new BadRequestException("Role not granted");

        return true;
    }

    public string GetPermissions()
    {
        var permissions = _permissionManager.GetAllPermissions().ToList();

        var jsonOptions = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(permissions, jsonOptions);

        return json;
    }
}