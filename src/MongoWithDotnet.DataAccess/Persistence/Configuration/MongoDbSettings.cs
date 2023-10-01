namespace MongoWithDotnet.DataAccess.Persistence.Configuration;

public class MongoDbSettings : IMongoDbSettings
{
    public string? DefaultDatabaseName { get; set; }

    public string? MongoDbConnection { get; set; }
}