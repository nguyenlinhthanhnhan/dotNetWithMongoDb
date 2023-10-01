namespace MongoWithDotnet.DataAccess.Persistence.Configuration;

public interface IMongoDbSettings
{
    string? DefaultDatabaseName { get; set; }

    string? MongoDbConnection { get; set; }
}