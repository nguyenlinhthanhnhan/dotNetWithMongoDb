using MongoDB.Driver;
using MongoWithDotnet.Core.Const;
using MongoWithDotnet.Core.Entities;
using MongoWithDotnet.DataAccess.Authorization;
using MongoWithDotnet.DataAccess.MQL;
using MongoWithDotnet.DataAccess.Persistence;

namespace MongoWithDotnet.DataAccess.Repositories.Impl;

public class ExampleRepository : BaseRepository<ExampleEntity>,
    IExampleRepository
{
    private readonly IPermissionManager _permissionManager;

    public ExampleRepository(MongoDbContext mongoDbContext, IPermissionManager permissionManager) : base(mongoDbContext)
    {
        var context = new MongoDbContext(new MongoClient(), AppDatabaseConst.ExampleDatabase1);
        MongoDbContext = context;
        _permissionManager = permissionManager;
    }

    public async Task<ExampleEntity> CustomGetItemAsync(FilterDefinition<ExampleEntity> filter)
    {
        var item = await Collection.SingleAsync(filter, null, 0);
        return item;
    }

    // Demo get data from multiple databases
    public async Task<List<ExampleEntity>> GetFromAnotherDb()
    {
        var p = _permissionManager.GetAllPermissions();
        MongoDbContext = new MongoDbContext(new MongoClient(), AppDatabaseConst.ExampleDatabase2);
        var f = await GetAllAsync(Builders<ExampleEntity>.Filter.Empty);
        MongoDbContext = new MongoDbContext(new MongoClient(), AppDatabaseConst.ExampleDatabase1);
        var x = await GetAllAsync(Builders<ExampleEntity>.Filter.Empty);
        return null;
    }
}