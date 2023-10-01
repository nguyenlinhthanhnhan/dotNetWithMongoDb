using MongoWithDotnet.Core.Entities;

namespace MongoWithDotnet.DataAccess.Repositories;

/// <summary>
/// Example repository.
/// </summary>
public interface IExampleRepository : IBaseRepository<ExampleEntity>
{
    Task<List<ExampleEntity>> GetFromAnotherDb();
}