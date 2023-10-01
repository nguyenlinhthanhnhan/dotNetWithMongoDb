using MongoDB.Driver;
using MongoWithDotnet.Application.Services.AuthenticationService.DTO;
using MongoWithDotnet.Core.Entities;

namespace MongoWithDotnet.Application.Services.AuthorizationService;

public interface IRoleManager
{
    Task<List<Role>> GetAll();
    Task<Role> GetFirst(FilterDefinition<Role> filter);
    Task<Role> Create(CreateRoleDto roleDto);
    Task<Role> Update(UpdateRoleDto roleDto, string id);
    Task<bool> Delete(string id);
}