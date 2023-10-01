using MongoWithDotnet.Application.Services.AuthenticationService.DTO;
using MongoWithDotnet.Core.Entities;

namespace MongoWithDotnet.Application.Services.AuthenticationService.Profile;

public class UserProfile : AutoMapper.Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, User>();
    }
}