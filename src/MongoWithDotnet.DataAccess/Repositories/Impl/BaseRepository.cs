using System.Linq.Expressions;
using MongoDB.Driver;
using MongoWithDotnet.Core.Common;
using MongoWithDotnet.DataAccess.Helpers.Pagination.DTO;
using MongoWithDotnet.DataAccess.MQL;
using MongoWithDotnet.DataAccess.Persistence;
using MongoWithDotnet.Shared.DTO;

namespace MongoWithDotnet.DataAccess.Repositories.Impl;

public abstract class BaseRepository<TNonSqlDocument> : IBaseRepository<TNonSqlDocument>
    where TNonSqlDocument : BaseEntity
{
    protected MongoDbContext MongoDbContext;

    protected IMongoCollection<TNonSqlDocument> Collection => MongoDbContext.GetCollection<TNonSqlDocument>();

    protected BaseRepository(MongoDbContext mongoDbContext)
    {
        MongoDbContext = mongoDbContext;
    }

    public virtual async Task<TNonSqlDocument> GetFirstAsync(FilterDefinition<TNonSqlDocument> filter)
    {
        var item = await Collection.SingleAsync(filter, null, 0);
        return item;
    }

    public virtual Task<TNonSqlDocument> InsertAsync(TNonSqlDocument document)
    {
        return Collection.InsertAsync(document);
    }

    public Task<List<TNonSqlDocument>> InsertManyAsync(List<TNonSqlDocument> documents)
    {
        return Collection.BulkInsertAsync(documents);
    }

    public virtual async Task<List<TNonSqlDocument>> GetAllAsync(FilterDefinition<TNonSqlDocument>? filter)
    {
        var items = await Collection.FindAsync(filter);
        return items.ToList();
    }

    public virtual async Task<PageDto<TNonSqlDocument>> GetAllWithPagingAsync(PageOptionDto optionDto,
        FilterDefinition<TNonSqlDocument>? filter, SortDefinition<TNonSqlDocument>? sort)
    {
        sort ??= Builders<TNonSqlDocument>.Sort.Descending(x => x.Id);
        return await Collection.Paginate(optionDto, filter, sort);
    }

    public virtual async Task<TNonSqlDocument> UpdateAsync(TNonSqlDocument document)
    {
        var filter = Builders<TNonSqlDocument>.Filter.Eq(x => x.Id, document.Id);
        await Collection.ReplaceOneAsync(filter, document);
        return document;
    }

    public virtual async Task<bool> DeleteAsync(string id)
    {
        var item = await Collection.DeleteAsync(id);
        return item;
    }

    public async Task<List<TNonSqlDocument>> UpdateManyAsync(List<TNonSqlDocument> documents)
    {
        var result = await Collection.UpdateManyAsync(documents);
        return result;
    }

    public async Task DeleteManyAsync(List<TNonSqlDocument> documents)
    {
        await Collection.BulkDeleteAsync(documents);
    }
}