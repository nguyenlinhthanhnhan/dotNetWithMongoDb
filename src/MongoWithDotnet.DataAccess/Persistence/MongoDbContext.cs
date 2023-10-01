using MongoDB.Driver;
using MongoWithDotnet.Core.Common;
using MongoWithDotnet.Core.Entities;
using MongoWithDotnet.DataAccess.Persistence.Configuration;

namespace MongoWithDotnet.DataAccess.Persistence;

/// <summary>
/// MongoDb context.
/// </summary>
public class MongoDbContext
{
    private readonly IMongoDatabase _mongoDatabase;

    public MongoDbContext(IMongoClient mongoClient, IMongoDbSettings settings)
    {
        _mongoDatabase = mongoClient.GetDatabase(settings.DefaultDatabaseName ??
                                                 throw new InvalidOperationException("Database not found"));
    }

    public MongoDbContext(IMongoClient mongoClient, string databaseName)
    {
        _mongoDatabase = mongoClient.GetDatabase(databaseName);
    }

    public IMongoCollection<ExampleEntity> Example => GetCollection<ExampleEntity>();

    public IMongoCollection<TMongoDocument> GetCollection<TMongoDocument>() where TMongoDocument : BaseEntity
    {
        return _mongoDatabase.GetCollection<TMongoDocument>(typeof(TMongoDocument).Name);
    }
}