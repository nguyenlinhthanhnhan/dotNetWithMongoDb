using AutoMapper;
using MongoDB.Driver;
using MongoWithDotnet.Application.Exceptions;
using MongoWithDotnet.Application.Services.AuthenticationService.DTO;
using MongoWithDotnet.Core.Entities;
using MongoWithDotnet.DataAccess.Authorization;
using MongoWithDotnet.DataAccess.Repositories;

namespace MongoWithDotnet.Application.Services.AuthorizationService.Impl;

public class RoleManager : IRoleManager
{
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionManager _permissionManager;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IMapper _mapper;

    public RoleManager(IRoleRepository roleRepository, IPermissionManager permissionManager, IMapper mapper,
        IPermissionRepository permissionRepository)
    {
        _roleRepository = roleRepository;
        _permissionManager = permissionManager;
        _mapper = mapper;
        _permissionRepository = permissionRepository;
    }

    public async Task<List<Role>> GetAll()
    {
        return await _roleRepository.GetAllAsync(null);
    }

    public async Task<Role> GetFirst(FilterDefinition<Role> filter)
    {
        return await _roleRepository.GetFirstAsync(filter);
    }

    public Task<Role> Create(CreateRoleDto roleDto)
    {
        var role = _mapper.Map<Role>(roleDto);
        return _roleRepository.InsertAsync(role);
    }

    public Task<Role> Update(UpdateRoleDto roleDto, string id)
    {
        var updatedRole = _roleRepository.GetFirstAsync(Builders<Role>.Filter.Eq(x => x.Id, id));

        if (updatedRole == null) throw new NotFoundException("Role not found");

        var role = _mapper.Map<Role>(roleDto);
        return _roleRepository.UpdateAsync(role);
    }

    public Task<bool> Delete(string id)
    {
        return _roleRepository.DeleteAsync(id);
    }
}