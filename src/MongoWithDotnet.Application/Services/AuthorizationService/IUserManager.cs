using MongoDB.Driver;
using MongoWithDotnet.Application.Services.AuthenticationService.DTO;
using MongoWithDotnet.Core.Entities;

namespace MongoWithDotnet.Application.Services.AuthorizationService;

public interface IUserManager
{
    Task<List<User>> GetAll();

    Task<User> GetFirst(FilterDefinition<User> filter);

    Task<User> Create(CreateUserDto userDto);

    Task<User> Update(UpdateUserDto userDto, string id);

    Task<bool> Delete(string id);
}