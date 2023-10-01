using MongoWithDotnet.Core.Entities;
using MongoWithDotnet.DataAccess.Persistence;

namespace MongoWithDotnet.DataAccess.Repositories.Impl;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(MongoDbContext mongoDbContext) : base(mongoDbContext)
    {
    }
}