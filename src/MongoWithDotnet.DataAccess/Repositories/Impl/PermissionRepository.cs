using MongoWithDotnet.Core.Entities;
using MongoWithDotnet.DataAccess.Persistence;

namespace MongoWithDotnet.DataAccess.Repositories.Impl;

public class PermissionRepository : BaseRepository<ApplicationPermission>, IPermissionRepository
{
    public PermissionRepository(MongoDbContext mongoDbContext) : base(mongoDbContext)
    {
    }
}