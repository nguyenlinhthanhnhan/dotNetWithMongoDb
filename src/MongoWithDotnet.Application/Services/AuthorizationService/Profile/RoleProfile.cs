using MongoWithDotnet.Application.Services.AuthenticationService.DTO;
using MongoWithDotnet.Core.Entities;

namespace MongoWithDotnet.Application.Services.AuthorizationService.Profile;

public class RoleProfile : AutoMapper.Profile
{
    public RoleProfile()
    {
        CreateMap<CreateRoleDto, Role>();
    }
}