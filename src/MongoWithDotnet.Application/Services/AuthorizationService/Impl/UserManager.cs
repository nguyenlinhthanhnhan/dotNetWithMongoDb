using AutoMapper;
using MongoDB.Driver;
using MongoWithDotnet.Application.Exceptions;
using MongoWithDotnet.Application.Services.AuthenticationService.DTO;
using MongoWithDotnet.Core.Entities;
using MongoWithDotnet.DataAccess.Repositories;

namespace MongoWithDotnet.Application.Services.AuthorizationService.Impl;

public class UserManager : IUserManager
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserManager(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<User>> GetAll()
    {
        return await _userRepository.GetAllAsync(null);
    }

    public async Task<User> GetFirst(FilterDefinition<User> filter)
    {
        return await _userRepository.GetFirstAsync(filter);
    }

    public async Task<User> Create(CreateUserDto userDto)
    {
        // WARNING: This is a very bad practice. You should never store passwords in plain text.
        var user = _mapper.Map<User>(userDto);
        return await _userRepository.InsertAsync(user);
    }

    public async Task<User> Update(UpdateUserDto userDto, string id)
    {
        var updatedUser = _userRepository.GetFirstAsync(Builders<User>.Filter.Eq(x => x.Id, id));
        if (updatedUser == null) throw new NotFoundException("User not found");
        var user = _mapper.Map<User>(userDto);
        return await _userRepository.UpdateAsync(user);
    }

    public async Task<bool> Delete(string id)
    {
        return await _userRepository.DeleteAsync(id);
    }
}