using MongoWithDotnet.Core.Entities;
using MongoWithDotnet.DataAccess.Persistence;

namespace MongoWithDotnet.DataAccess.Repositories.Impl;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(MongoDbContext mongoDbContext) : base(mongoDbContext)
    {
    }
}